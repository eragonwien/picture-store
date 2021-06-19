import { MaterialIcons } from '@expo/vector-icons';
import React from 'react';
import { StyleSheet } from "react-native";
import { TouchableOpacity } from 'react-native-gesture-handler';
import { ActivityIndicator, useTheme } from 'react-native-paper';
import { View } from '../components/core/View';
import { Text } from '../components/core/Text';
import { navigationBarIconSize } from '../constants/Sizes';
import { ImageBrowser } from 'expo-image-picker-multiple';

export const TabUploadScreenImagePickerStack = ({ navigation }: { navigation: any }) => {

    const theme = useTheme();
    const styles = createStyles();

    React.useLayoutEffect(() => {
        setTitle(0);
    }, [navigation]);

    const setTitle = (selectionCount: number) => {
        navigation.setOptions({
            title: selectionCount > 1 ? `${selectionCount} files selected` : `${selectionCount} file selected`,
        });
    }

    const setLoading = () => {
        navigation.setOptions({
            headerRight: () => (
                <ActivityIndicator animating={true} color={theme.colors.text} />
            ),
        });
    }

    const setSaveButton = (onSubmit?: () => void) => {
        navigation.setOptions({
            headerRight: () => (
                <TouchableOpacity onPress={onSubmit}>
                    <MaterialIcons
                        name="save"
                        size={navigationBarIconSize}
                        color={theme.colors.text}
                    />
                </TouchableOpacity>
            ),
        });
    }

    return (
        <View style={styles.container}>
            <ImageBrowser
                onChange={(num: number, onSubmit: () => void) => {
                    setSaveButton(onSubmit);
                    setTitle(num);
                }}
                callback={(callback: any) => {
                    setLoading();

                    callback
                        .then(async (photos: any) => {
                            const cPhotos = [];

                            for (const photo of photos) {
                                cPhotos.push({
                                    uri: photo.uri,
                                    name: photo.filename,
                                    type: 'image/jpg',
                                });
                            }

                            alert(JSON.stringify(cPhotos));
                            setSaveButton();
                        })
                        .catch((error: any) => console.log(error));
                }}
                renderSelectedComponent={(count: number) => (
                    <View style={styles.selected}>
                        <MaterialIcons
                            name="check-circle-outline"
                            size={navigationBarIconSize}
                            color={theme.colors.text} />
                    </View>
                )}
                emptyStayComponent={<Text>Empty =(</Text>}
            />
        </View>
    );
}

const createStyles = () => {
    const theme = useTheme();
    return StyleSheet.create({
        container: {
            height: '100%',
        },
        selected: {
            backgroundColor: theme.colors.primary,
            display: 'flex',
            flexDirection: 'row-reverse'
        }
    });
};