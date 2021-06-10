import { Theme, useTheme } from '@react-navigation/native';
import React from 'react';
import { Modal, StyleSheet } from 'react-native';
import { mediumSize, smallSize } from './Themed';
import { ImageBrowser } from 'expo-image-picker-multiple';
import { View } from './core/View';

export const ImageGalleryDialog = ({ visible, setVisible }: { visible: boolean, setVisible: any }) => {

    const theme = useTheme();
    const styles = createStyles(theme);

    return (
        <View>
            <Modal
                animationType="slide"
                visible={visible}
                onRequestClose={() => {
                    setVisible(false);
                }}
            >
                <ImageBrowser
                    max={4}
                    onChange={(num, onSubmit) => {

                    }}
                    callback={(callback) => {

                    }}
                />
            </Modal>
        </View>
    );
}

const createStyles = (theme: Theme) => {
    return StyleSheet.create({
        container: {
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "center",
            margin: smallSize,
            borderColor: theme.colors.text,
            borderWidth: smallSize,
            borderRadius: mediumSize,
            borderStyle: "dashed",
        },
    });
};