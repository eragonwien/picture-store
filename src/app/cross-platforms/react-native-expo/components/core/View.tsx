import React from 'react';
import { View as DefaultView } from "react-native";

export const View = (props: DefaultView["props"]) => {
    const { style, ...otherProps } = props;

    // style = { [{ color, fontSize: largeSize, fontWeight: "bold" }, style]}

    return <DefaultView style={style} {...otherProps} />;
};