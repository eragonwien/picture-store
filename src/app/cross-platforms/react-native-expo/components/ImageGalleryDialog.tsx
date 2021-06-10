import { Theme, useTheme } from '@react-navigation/native';
import React from 'react';
import { Modal, StyleSheet } from 'react-native';
import { View } from './core/View';
import { Button, Dialog, Paragraph, Portal } from 'react-native-paper';
import { smallSize, mediumSize } from '../constants/Sizes';

export const ImageGalleryDialog = ({ visible, setVisible }: { visible: boolean, setVisible: any }) => {

    const theme = useTheme();
    const styles = createStyles(theme);

    const onDismiss = () => setVisible(false);

    return (
        <View>
            <Portal>
                <Dialog visible={visible} onDismiss={onDismiss}>
                    <Dialog.Title>Alert</Dialog.Title>
                    <Dialog.Content>
                        <Paragraph>This is simple dialog</Paragraph>
                    </Dialog.Content>
                    <Dialog.Actions>
                        <Button onPress={onDismiss}>Done</Button>
                    </Dialog.Actions>
                </Dialog>
            </Portal>
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