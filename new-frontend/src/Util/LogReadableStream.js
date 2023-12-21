export default async function logReadableStream(readableStream) {
    const reader = readableStream.getReader();
    const textDecoder = new TextDecoder();

    try {
        while (true) {
            const { done, value } = await reader.read();

            if (done) {
                break;
            }

            const chunk = textDecoder.decode(value, { stream: true });
            console.log(chunk);
        }

        // Optionally, you can handle the end of the stream here
        console.log("Stream fully consumed");
    } catch (error) {
        console.error("Error reading the stream:", error);
    } finally {
        reader.releaseLock();
    }
}