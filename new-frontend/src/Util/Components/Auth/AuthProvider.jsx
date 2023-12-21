import { useEffect, useImperativeHandle } from "react";

import HandleInvalidAccesstoken from "./HandleInvalidAccessToken";
import HandleInvalidRefreshToken from "./HandleInvalidRefreshToken";
import HandleLogin from "./HandleLogin";
import HandleLogout from "./HandleLogout";

const AuthProvider = React.forwardRef(({children}, ref) => {
    useImperativeHandle(ref, () => ({
        HandleInvalidAccessToken() {
            HandleInvalidAccesstoken()
        },
        HandleInvalidRefreshToken() {
            HandleInvalidRefreshToken()
        },
        HandleLogin(email, password) {
            HandleLogin(email, password)
        },
        HandleLogout() {
            HandleLogout()
        }
    }))

    useEffect(() => {
        HandleAccessTokenChanged();
    }, [accessToken])

    useEffect(() => {
        HandleRefreshTokenChanged();
    }, [refreshToken])

    return (
        <>
            {children}
        </>
    )
})

export default AuthProvider;