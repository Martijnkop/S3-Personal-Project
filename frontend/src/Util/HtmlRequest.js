function HtmlRequest(method, url, body, auth) {
    const fetchInput = async () => {
        let headers;
        if (auth == null) {
            headers = {'Content-Type': 'application/json'}
        }
        else {
            headers = {'Content-Type': 'application/json','Authorization': `Bearer ${auth}`}
        }
        const response = await fetch(url, {
        method: method,
        body: JSON.stringify(body),
        headers: headers
      });

      return response
      }
    let a = fetchInput().then(response => {
        return response;
    })
    return a;
}

export default HtmlRequest