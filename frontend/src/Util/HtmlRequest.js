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
    let res = fetchInput().then(response => {
        return response;
    })
    return res;
}

export default HtmlRequest