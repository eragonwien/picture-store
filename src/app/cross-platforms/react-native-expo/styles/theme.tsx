import { DefaultTheme, DarkTheme } from 'react-native-paper';
import { StyleSheet } from "react-native";

export const lightTheme: ReactNativePaper.Theme = {
    ...DefaultTheme,
    animation: {
        ...DefaultTheme.animation,
    },
    colors: {
        ...DefaultTheme.colors,
        primary: "#8e44ad",
        error: "#c0392b",
        background: "#ecf0f1",
        text: "white",
    },
    dark: DefaultTheme.dark,
    fonts: {
        ...DefaultTheme.fonts,
    },
    mode: DefaultTheme.mode,
    roundness: DefaultTheme.roundness,
}

export const darkTheme: ReactNativePaper.Theme = {
    ...DarkTheme,
    animation: {
        ...DarkTheme.animation,
    },
    colors: {
        ...DarkTheme.colors,
    },
    dark: DarkTheme.dark,
    fonts: {
        ...DarkTheme.fonts,
    },
    mode: DarkTheme.mode,
    roundness: DarkTheme.roundness,
}