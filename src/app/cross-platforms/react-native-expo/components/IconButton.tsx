import React from "react";
import { StyleSheet, Text, TouchableOpacity } from "react-native";

export const IconButton = ({ icon, text }: { icon?: any; text?: string }) => {
  return (
    <TouchableOpacity style={styles.container}>
      {icon}
      {text && <Text>{text}</Text>}
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  container: {
    display: "flex",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
  },
});
