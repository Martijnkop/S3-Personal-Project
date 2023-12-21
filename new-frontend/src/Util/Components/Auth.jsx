import { useRef } from "react"
import AuthProvider from "./Auth/AuthProvider"
import { useDispatch } from "react-redux"
import { setAuthProvider } from "../Store/authProviderSlice"

function Auth({children}) {
    const dispatch = useDispatch()

    const authRef = useRef()
    dispatch(setAuthProvider(authRef))

    return (
    <AuthProvider ref={authRef}>
        {children}
    </AuthProvider>
    )
}

export default Auth