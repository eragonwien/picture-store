import React from 'react';
import { View as DefaultView } from "react-native";
import { useTheme } from 'react-native-paper';

export const View = (props: DefaultView["props"]) => {
    const { style, ...otherProps } = props;
    const theme = useTheme();

    return <DefaultView style={[{ backgroundColor: theme.colors.background }, style]} {...otherProps} />;
};