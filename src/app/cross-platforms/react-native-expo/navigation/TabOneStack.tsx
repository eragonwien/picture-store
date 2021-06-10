import React from 'react';
import { TabOneScreenName } from '../constants/Screens';
import { createStackNavigator } from '@react-navigation/stack';
import TabOneScreen from '../screens/TabOneScreen';

const TabOneStack = createStackNavigator();

export const TabOneStackScreen = () => {
    return (
        <TabOneStack.Navigator>
            <TabOneStack.Screen
                name={TabOneScreenName}
                component={TabOneScreen} />
        </TabOneStack.Navigator>
    );
};


