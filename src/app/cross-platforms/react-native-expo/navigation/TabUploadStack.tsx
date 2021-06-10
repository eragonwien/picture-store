import React from 'react';
import { TabUploadScreen } from '../screens/TabUploadScreen';
import { TabUploadScreenName } from '../constants/Screens';
import { createStackNavigator } from '@react-navigation/stack';

const TabUploadStack = createStackNavigator();

export const TabUploadStackScreen = () => {
    return (
        <TabUploadStack.Navigator>
            <TabUploadStack.Screen
                name={TabUploadScreenName}
                component={TabUploadScreen} />
        </TabUploadStack.Navigator>
    );
};


