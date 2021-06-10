import React from 'react';
import { Text as DefaultText } from "react-native";

export const Text = (props: DefaultText["props"]) => {
    const { style, ...otherProps } = props;

    return <DefaultText style={style} {...otherProps} />;
};
