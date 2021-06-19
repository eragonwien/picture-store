import * as React from "react";
import { MaterialIcons } from "@expo/vector-icons";
import { StyleSheet } from "react-native";
import { TouchableOpacity } from "react-native";
import { useTheme } from 'react-native-paper';
import { smallSize, mediumSize } from '../constants/Sizes';
import { useNavigation } from '@react-navigation/native';
import { TabUploadScreenImagePickerStackName } from '../constants/Screens';

export const TabUploadScreenImageUploadView = ({ height }: { height: number | string }) => {
  const theme = useTheme();
  const styles = createStyles(theme);
  const navigation = useNavigation();

  return (
    <TouchableOpacity
      style={{ ...styles.container, height: height }}
      onPress={() => navigation.navigate(TabUploadScreenImagePickerStackName)}
    >
      <MaterialIcons name="file-upload" size={64} color={theme.colors.primary} />
    </TouchableOpacity>
  );
};

const createStyles = (theme: ReactNativePaper.Theme) => {
  return StyleSheet.create({
    container: {
      display: "flex",
      flexDirection: "column",
      alignItems: "center",
      justifyContent: "center",
      margin: smallSize,
      borderColor: theme.colors.primary,
      borderWidth: smallSize,
      borderRadius: mediumSize,
      borderStyle: "dashed",
    },
  });
};
