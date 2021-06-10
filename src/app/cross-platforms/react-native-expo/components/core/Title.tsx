import React from 'react';
import { largeSize } from '../../constants/Sizes';
import { Text } from "../core/Text";
import { Text as DefaultText } from "react-native";
import { useTheme } from 'react-native-paper';

export const Title = (props: DefaultText["props"]) => {
    const { style, ...otherProps } = props;
    const theme = useTheme();

    return (
        <Text
            style={[{ fontSize: largeSize, fontWeight: "bold" }, style]}
            {...otherProps}
        />
    );
};