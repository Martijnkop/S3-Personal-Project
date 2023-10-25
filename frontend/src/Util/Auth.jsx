import React, { useImperativeHandle, useEffect } from 'react'
import HtmlRequest from './HtmlRequest'

const Auth = React.forwardRef(({children, setAccessToken, setRefreshToken}, ref) => {

    useImperativeHandle(ref, () => ({
        HandleLogin(email, password) {
          HandleLogin(email, password)
        }
      }))

    function HandleLogin(email, password) {
        const response = HtmlRequest(
            'POST',
            'https://auth.localhost:6841/accounts/login',
            {
                email: email,
                password: password
            }).then(function(res) {
                res.json().then(response => {
                    if (response.refreshToken != null) {
                        localStorage.setItem('refreshToken', response.refreshToken.replace(/-/g, ""))
                        setRefreshToken(response.refreshToken.replace(/-/g, ""))
                    }
                    if (response.accessToken != undefined) {
                        localStorage.setItem('accessToken', response.accessToken)
                        setAccessToken(response.accessToken)
                }
                })
            })
    }

    useEffect(() => {
        let temp1 = localStorage.getItem('refreshToken')
        let refreshToken;
        if (temp1 != null) {
            refreshToken = (temp1.replace(/-/g, ""))
            setRefreshToken(refreshToken)
        }
        let temp2 = localStorage.getItem('accessToken')
        let accessToken;
        if (temp2 != null) {
            accessToken = (temp2.replace(/-/g, ""))
            setAccessToken(accessToken)
        }
        if (refreshToken != null && accessToken == null) {
        const response = HtmlRequest(
            'POST',
            'https://auth.localhost:6841/accounts/validateRefreshToken',
            {refreshToken: refreshToken}).then(function(res) {
                res.json().then(response => {
                    if (response.refreshToken != null) {
                        localStorage.setItem('refreshToken', response.refreshToken.replace(/-/g, ""))
                        setRefreshToken(response.refreshToken)
                    }
                    if (response.accessToken != undefined) {
                        localStorage.setItem('accessToken', response.accessToken)
                        setAccessToken(response.accessToken)
                }
                })
            })
        }
      })

    return (
        <>
        {children}
        </>
    )
})

export default Auth;