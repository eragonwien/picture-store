import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { TabUploadScreen } from '../screens/TabUploadScreen';
import TabOneScreen from '../screens/TabOneScreen';
import TabTwoScreen from '../screens/TabTwoScreen';

const Tab = createBottomTabNavigator();

export const Navigation = () => {
    return (
        <NavigationContainer>
            <Tab.Navigator>
                <Tab.Screen name="TabOneScreen" component={TabOneScreen} />
                <Tab.Screen name="TabUploadScreen" component={TabUploadScreen} />
                <Tab.Screen name="TabTwoScreen" component={TabTwoScreen} />
            </Tab.Navigator>
        </NavigationContainer >
    );
}
