import { MaterialIcons } from '@expo/vector-icons';
import { useTheme } from '@react-navigation/native';
import React from 'react';
import { StyleProp, TextStyle } from 'react-native';

export const ProgressIcon = ({ progress, style }: { progress: number, style?: StyleProp<TextStyle> }) => {

    const theme = useTheme();

    switch (progress) {
        case 1:
            return (
                <MaterialIcons
                    style={style}
                    name="check-circle-outline"
                    size={32}
                    color="green" />
            );
        default:
            return (
                <MaterialIcons
                    style={style}
                    name="schedule"
                    size={32}
                    color={theme.colors.text} />
            );
    }
}