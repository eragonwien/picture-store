import React from 'react';
import { TabUploadScreenHomeStack } from '../screens/TabUploadScreen.HomeStack';
import { TabUploadScreenHomeStackName, TabUploadScreenHomeStackTitle, TabUploadScreenImagePickerStackName } from '../constants/Screens';
import { createStackNavigator } from '@react-navigation/stack';
import { TabUploadScreenImagePickerStack } from '../screens/TabUploadScreen.ImagePickerStack';
import { useTheme } from 'react-native-paper';
import { StyleSheet } from "react-native";

const TabUploadStack = createStackNavigator();

export const TabUploadStackScreen = () => {

    const theme = useTheme();
    const styles = createStyles();

    return (
        <TabUploadStack.Navigator>
            <TabUploadStack.Screen
                name={TabUploadScreenHomeStackName}
                component={TabUploadScreenHomeStack}
                options={{
                    title: TabUploadScreenHomeStackTitle,
                    headerStyle: styles.header,
                    headerTitleStyle: styles.title,
                    headerTintColor: theme.colors.text,
                }} />
            <TabUploadStack.Screen
                name={TabUploadScreenImagePickerStackName}
                component={TabUploadScreenImagePickerStack}
                options={{
                    title: "",
                    headerStyle: styles.header,
                    headerTitleStyle: styles.title,
                    headerTintColor: theme.colors.text,
                    headerRightContainerStyle: { paddingRight: 16 }
                }} />
        </TabUploadStack.Navigator>
    );
};

const createStyles = () => {
    const theme = useTheme();
    return StyleSheet.create({
        header: {
            backgroundColor: theme.colors.primary,
        },
        title: {
            color: theme.colors.text,
        }
    });
};


