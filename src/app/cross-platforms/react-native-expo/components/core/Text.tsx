import React from 'react';
import { Text as DefaultText } from "react-native";
import { useTheme } from 'react-native-paper';

export const Text = (props: DefaultText["props"]) => {
    const { style, ...otherProps } = props;
    const theme = useTheme();

    return <DefaultText style={[{ color: theme.colors.text }, style]} {...otherProps} />;
};
