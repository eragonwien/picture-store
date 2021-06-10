import React from 'react';
import { useThemeColor, largeSize, TextProps } from '../Themed';
import { Text } from "../core/Text";

export const Title = (props: TextProps) => {
    const { style, lightColor, darkColor, ...otherProps } = props;
    const color = useThemeColor({ light: lightColor, dark: darkColor }, "text");

    return (
        <Text
            style={[{ color, fontSize: largeSize, fontWeight: "bold" }, style]}
            {...otherProps}
        />
    );
};