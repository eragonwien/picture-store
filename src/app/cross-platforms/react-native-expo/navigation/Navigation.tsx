import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import TabOneScreen from '../screens/TabOneScreen';
import TabTwoScreen from '../screens/TabTwoScreen';
import { createMaterialBottomTabNavigator } from '@react-navigation/material-bottom-tabs';
import { MaterialCommunityIcons, MaterialIcons } from '@expo/vector-icons';
import { useTheme } from 'react-native-paper';
import { StyleSheet } from "react-native";
import { TabUploadScreenHomeStackName, TabOneScreenName, TabTwoScreenName, TabUploadScreenHomeStackTitle } from '../constants/Screens';
import { TabUploadStackScreen } from './TabUploadStack';
import { TabTwoStackScreen } from './TabTwoStack';
import { TabOneStackScreen } from './TabOneStack';
import { navigationBarIconSize } from '../constants/Sizes';

const Tab = createMaterialBottomTabNavigator();

export const Navigation = () => {

    const theme = useTheme();
    const styles = createStyles();

    return (
        <NavigationContainer>
            <Tab.Navigator
                initialRouteName={TabUploadScreenHomeStackName}
                activeColor={theme.colors.text}
                barStyle={styles.bar}
            >
                <Tab.Screen
                    name={TabOneScreenName}
                    component={TabOneStackScreen}
                    options={{
                        tabBarIcon: ({ color }) => <MaterialIcons name="collections" color={color} size={navigationBarIconSize} />,
                        title: "One",
                    }}
                />
                <Tab.Screen
                    name={TabUploadScreenHomeStackName}
                    component={TabUploadStackScreen}
                    options={{
                        tabBarIcon: ({ color }) => <MaterialIcons name="cloud-upload" color={color} size={navigationBarIconSize} />,
                        title: TabUploadScreenHomeStackTitle,
                    }}
                />
                <Tab.Screen
                    name={TabTwoScreenName}
                    component={TabTwoStackScreen}
                    options={{
                        tabBarIcon: ({ color }) => <MaterialCommunityIcons name="cog" color={color} size={navigationBarIconSize} />,
                        title: "Two",
                    }}
                />
            </Tab.Navigator>
        </NavigationContainer>
    );
}

const createStyles = () => {
    const theme = useTheme();
    return StyleSheet.create({
        bar: {
            backgroundColor: theme.colors.primary,
        },
    });
};