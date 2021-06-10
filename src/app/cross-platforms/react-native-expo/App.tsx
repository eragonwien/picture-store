import 'react-native-gesture-handler';
import { StatusBar } from 'expo-status-bar';
import React from 'react';
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
    <PaperProvider theme={colorScheme === "dark" ? darkTheme : lightTheme}>
      <Navigation />
      <StatusBar />
    </PaperProvider>
  );
}
