import { createSlice } from "@reduxjs/toolkit";

export const authProviderSlice = createSlice({
    name: 'authProvider',
    initialState: {
        value: {}
    },
    reducers: {
        setAuthProvider: (state, action) => {
            state.value = action.payload
        }
    }
})

export const { setAuthProvider } = authProviderSlice.actions

export default authProviderSlice.reducer