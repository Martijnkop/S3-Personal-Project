import { createSlice } from "@reduxjs/toolkit";

export const authSlice = createSlice({
    name: 'auth',
    initialState: {
        value: {
            validRefreshToken: false,
            refreshToken: '',
            validAccessToken: false,
            accessToken: '',
        }
    },
    reducers: {
        setRefreshToken: (state, action) => {
            let oldState = state.value;
            state.value = {
                validRefreshToken: true,
                refreshToken: action.payload,
                validAccessToken: oldState.validAccessToken,
                accessToken: oldState.accessToken
            }
        },
        setAccessToken: (state, action) => {
            let oldState = state.value;
            state.value = {
                validRefreshToken: oldState.validRefreshToken,
                refreshToken: oldState.refreshToken,
                validAccessToken: true,
                accessToken: action.payload,
            }
        },
        removeRefreshToken: (state) => {
            let oldState = state.value;
            localStorage.setItem('refreshToken', '')
            state.value = {
                validRefreshToken: false,
                refreshToken: '',
                validAccessToken: oldState.validAccessToken,
                accessToken: oldState.accessToken
            }
        },
        removeAccessToken: (state) => {
            let oldState = state.value;
            localStorage.setItem('accessToken', '')
            state.value = {
                validRefreshToken: oldState.validRefreshToken,
                refreshToken: oldState.refreshToken,
                validAccessToken: false,
                accessToken: ''
            }
        }
    }
})

export const { setRefreshToken, setAccessToken, removeAccessToken, removeRefreshToken } = authSlice.actions

export default authSlice.reducer