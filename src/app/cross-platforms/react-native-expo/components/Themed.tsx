import * as React from "react";
import { Text as DefaultText, View as DefaultView } from "react-native";
import Colors from "../constants/Colors";
import useColorScheme from "../hooks/useColorScheme";

export const smallSize = 8;
export const mediumSize = 16;
export const largeSize = 32;

export const successColor = "";

export const useThemeColor = (
  props: { light?: string; dark?: string },
  colorName: keyof typeof Colors.light & keyof typeof Colors.dark
) => {
  const theme = useColorScheme();
  const colorFromProps = props[theme];

  if (colorFromProps) {
    return colorFromProps;
  } else {
    return Colors[theme][colorName];
  }
};

export type ThemeProps = {
  lightColor?: string;
  darkColor?: string;
};

export type TextProps = ThemeProps & DefaultText["props"];
export type ViewProps = ThemeProps & DefaultView["props"];


