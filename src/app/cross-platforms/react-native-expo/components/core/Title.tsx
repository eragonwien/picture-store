import React from 'react';
import { largeSize } from '../../constants/Sizes';
import { Text } from "../core/Text";
import { Text as DefaultText } from "react-native";

export const Title = (props: DefaultText["props"]) => {
    const { style, ...otherProps } = props;

    return (
        <Text
            style={[{ fontSize: largeSize, fontWeight: "bold" }, style]}
            {...otherProps}
        />
    );
};