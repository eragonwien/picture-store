import * as React from "react";
import { useState } from 'react';
import { MaterialIcons } from "@expo/vector-icons";
import { StyleSheet } from "react-native";
import { TouchableOpacity } from "react-native";
import { ImageGalleryDialog } from './ImageGalleryDialog';
import { Text } from './core/Text';
import { useTheme } from 'react-native-paper';
import { smallSize, mediumSize } from '../constants/Sizes';

export const TabUploadScreenImageUploadView = ({ height }: { height: number | string }) => {
  const theme = useTheme();
  const styles = createStyles(theme);

  const [galleryVisible, setGalleryVisible] = useState(false);

  return (
    <TouchableOpacity
      style={{ ...styles.container, height: height }}
      onPress={async () => {
        setGalleryVisible(true);
      }}
    >
      <MaterialIcons name="file-upload" size={64} color={theme.colors.text} />
      <Text>Upload</Text>
      <ImageGalleryDialog visible={galleryVisible} setVisible={setGalleryVisible} />
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
      borderColor: theme.colors.text,
      borderWidth: smallSize,
      borderRadius: mediumSize,
      borderStyle: "dashed",
    },
  });
};
