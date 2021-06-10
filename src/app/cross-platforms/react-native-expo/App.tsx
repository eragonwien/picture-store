import 'react-native-gesture-handler';
import { StatusBar } from 'expo-status-bar';
import React from 'react';
import { SafeAreaProvider } from 'react-native-safe-area-context';
import useCachedResources from './hooks/useCachedResources';
import useColorScheme from './hooks/useColorScheme';
import { Provider as PaperProvider } from 'react-native-paper';
import { lightTheme, darkTheme } from './styles/theme';
import { Navigation } from './navigation/Navigation';

export default function App() {
  const isLoadingComplete = useCachedResources();
  const colorScheme = useColorScheme();

  if (!isLoadingComplete)
    return null;

  return (
    <SafeAreaProvider>
      <PaperProvider theme={colorScheme === "dark" ? darkTheme : lightTheme}>
        <Navigation />
        <StatusBar />
      </PaperProvider>
    </SafeAreaProvider>

  );
}
