import store from '../Store/store'
import { removeAccessToken } from '../Store/authSlice';

function HtmlRequest(method, url, body, auth) {
    const fetchInput = async () => {
        let headers;
        if (auth == null) {
            headers = {'Content-Type': 'application/json'}
        }
        else {
            headers = {'Content-Type': 'application/json','Authorization': `Bearer ${auth}`}
        }
        let parsedBody = undefined;
        if (method != "GET") parsedBody = JSON.stringify(body)
        console.log(parsedBody)
        const response = await fetch(url, {
        method: method,
        body: parsedBody,
        headers: headers
      });

      return response
      }
    let res = fetchInput().then(response => {
        if (response.status == 401) {
            store.dispatch(removeAccessToken())
            console.log('removed access token')
        }
        return response;
    })
    
    return res;
}

export default HtmlRequest