import React from 'react';
import { ThemeProps, useThemeColor } from '../Themed';
import { Text as DefaultText } from "react-native";

export type TextProps = ThemeProps & DefaultText["props"];

export const Text = (props: TextProps) => {
    const { style, lightColor, darkColor, ...otherProps } = props;
    const color = useThemeColor({ light: lightColor, dark: darkColor }, "text");

    return <DefaultText style={[{ color }, style]} {...otherProps} />;
};
