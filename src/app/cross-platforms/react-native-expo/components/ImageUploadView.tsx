import { MaterialIcons } from "@expo/vector-icons";
import * as React from "react";
import { StyleSheet, Text, TouchableOpacity } from "react-native";

export const ImageUploadView = ({ height }: { height: number | string }) => {
  return (
    <TouchableOpacity
      style={{ ...styles.container, height: height }}
      onPress={async () => {}}
    >
      <MaterialIcons name="upload-file" size={64} color="black" />
      <Text>Upload</Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  container: {
    display: "flex",
    backgroundColor: "white",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center",
    margin: 8,
    borderColor: "grey",
    borderWidth: 8,
    borderRadius: 16,
    borderStyle: "dashed",
  },
});
