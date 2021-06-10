import React from 'react';
import { TabTwoScreenName } from '../constants/Screens';
import { createStackNavigator } from '@react-navigation/stack';
import TabTwoScreen from '../screens/TabTwoScreen';

const TabTwoStack = createStackNavigator();

export const TabTwoStackScreen = () => {
    return (
        <TabTwoStack.Navigator>
            <TabTwoStack.Screen
                name={TabTwoScreenName}
                component={TabTwoScreen} />
        </TabTwoStack.Navigator>
    );
};
