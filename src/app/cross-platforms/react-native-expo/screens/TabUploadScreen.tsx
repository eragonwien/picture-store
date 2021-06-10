import * as React from "react";
import { StyleSheet } from "react-native";
import { View } from '../components/core/View';
import { TabUploadScreenImageUploadView } from '../components/TabUploadScreenImageUploadView';

export const TabUploadScreen = ({ }: {}) => {

  const styles = createStyles();

  return (
    <View style={styles.container}>
      <TabUploadScreenImageUploadView height="30%" />
      <View style={styles.listContainer}></View>
    </View>
  );
};

const createStyles = () => {
  return StyleSheet.create({
    container: {
      width: "100%",
      height: "100%",
      display: "flex",
      justifyContent: "flex-start",
      alignContent: "center",
    },
    listContainer: {
      maxWidth: "100%",
      height: "65%",
      display: "flex",
      alignItems: "center",
      justifyContent: "flex-start",
      margin: 8,
    },
  });
};

const DATA = [
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba12",
    title: "First Item",
    progress: 1,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f6311",
    title: "Second Item",
    progress: 0.5,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d7210",
    title: "Third Item",
    progress: 0.1,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba9",
    title: "First Item",
    progress: 1,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f638",
    title: "Second Item",
    progress: 0.6,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d727",
    title: "Third Item",
    progress: 0.8,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba6",
    title: "First Item",
    progress: 0.4,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f635",
    title: "Second Item",
    progress: 0.2,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d724",
    title: "Third Item",
    progress: 0.7,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba3",
    title: "First Item",
    progress: 0.9,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f632",
    title: "Second Item",
    progress: 0.5,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d721",
    title: "Third Item",
    progress: 0.1,
    uri: "https://bulma.io/images/bulma-logo.png",
  },
];
