import { ThemeProvider } from "@emotion/react";
import { CssBaseline } from "@mui/material";

import { useSelector } from "react-redux";

import { lightTheme } from "../Theme/LightTheme";
import { darkTheme } from "../Theme/DarkTheme";

function Themer({ children }) {
    const activeTheme = useSelector((state) => state.theme.value)

    let theme;
    switch (activeTheme) {
        case 'dark':
            theme = darkTheme;
            break;
        case 'light':
            theme = lightTheme;
            break;
        default:
            // UPDATE: listen to web preferences
            theme = lightTheme;
    }

    return <ThemeProvider theme={theme}>
        <CssBaseline />
        {children}
    </ThemeProvider>
}

export default Themer;