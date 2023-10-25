import { useState } from "react";
import './Login.css';

function Login({ submit }) {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")

    const handleSubmit = (event) => {
        event.preventDefault()
        submit(email, password)
    }

    return (
        <div className="login">
            <form className="login-form" onSubmit={handleSubmit}>
                <label id="email-label" htmlFor="email">Email:</label>
                <input id="email" type="text" name="email" onChange={(event) => setEmail(event.target.value)} autoComplete="email"/>
                <label id="password-label" htmlFor="password">Password:</label>
                <input id="password" type="password" name="password" onChange={(event) => setPassword(event.target.value)} autoComplete="current-password"/>
                <input id="submit" type="submit" value="Login"/>
            </form>
        </div>
    )
}

export default Login;