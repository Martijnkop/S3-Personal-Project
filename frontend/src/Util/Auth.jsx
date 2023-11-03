import React, { useImperativeHandle, useEffect } from 'react'
import HtmlRequest from './HtmlRequest'
import { useDispatch, useSelector } from 'react-redux'
import { setAccessToken, setRefreshToken, removeAccessToken, removeRefreshToken } from './Store/authSlice'

const Auth = React.forwardRef(({children}, ref) => {
    const accessToken = useSelector((state) => state.auth.value.accessToken)
    const refreshToken = useSelector((state) => state.auth.value.refreshToken)
    const dispatch = useDispatch()

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
            }).then( res => {
            res.json().then(response => {
                    if (response.refreshToken != null) {
                        localStorage.setItem('refreshToken', response.refreshToken.replace(/-/g, ""))
                        dispatch(setRefreshToken(response.refreshToken.replace(/-/g, "")))
                    }
                    if (response.accessToken != undefined) {
                        localStorage.setItem('accessToken', response.accessToken)
                        dispatch(setAccessToken(response.accessToken))
                }
            })
        })
    }

    useEffect(() => {
        let temp1 = localStorage.getItem('refreshToken')
        let refreshToken;
        if (temp1 != null) {
            refreshToken = (temp1.replace(/-/g, ""))
            dispatch(setRefreshToken(refreshToken))
        }
        let temp2 = localStorage.getItem('accessToken')
        let accessToken;
        if (temp2 != null) {
            accessToken = (temp2.replace(/-/g, ""))
            dispatch(setAccessToken(accessToken))
        }
        if (refreshToken != null && (accessToken == null || accessToken == '')) {
        const response = HtmlRequest(
            'POST',
            'https://auth.localhost:6841/accounts/validateRefreshToken',
            {refreshToken: refreshToken}).then(function(res) {
                res.json().then(response => {
                    if (response.refreshToken != null) {
                        localStorage.setItem('refreshToken', response.refreshToken.replace(/-/g, ""))
                        dispatch(setRefreshToken(response.refreshToken))
                    } else {
                        dispatch(removeRefreshToken())
                    }
                    if (response.accessToken != undefined) {
                        localStorage.setItem('accessToken', response.accessToken)
                        dispatch(setAccessToken(response.accessToken))
                    } else {
                        dispatch(removeAccessToken())
                    }
                })
            })
        }
    },[accessToken, refreshToken])

    return (
        <>
        {children}
        </>
    )
})

export default Auth;