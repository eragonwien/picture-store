import { MaterialIcons } from '@expo/vector-icons';
import { Theme, useTheme } from '@react-navigation/native';
import * as React from "react";
import { Image, StyleSheet } from "react-native";
import { ScrollView } from 'react-native-gesture-handler';
import { ImageUploadView } from "../components/ImageUploadView";
import { ProgressIcon } from '../components/ProgressIcon';
import { View, largeSize, TitleText, Text } from "../components/Themed";

export const TabUploadScreen = ({ }: {}) => {

  const theme = useTheme();
  const styles = createStyles(theme);

  return (
    <View style={styles.container}>
      <ImageUploadView height={"30%"} />
      <View style={styles.listContainer}>
        <TitleText style={{ padding: 8, textAlign: "left", width: "100%" }}>
          Hochgeladene Bilder
        </TitleText>
      </View>
    </View>
  );
};

const createStyles = (theme: Theme) => {
  return StyleSheet.create({
    container: {
      width: "100%",
      height: "100%",
      display: "flex",
      justifyContent: "flex-start",
      alignContent: "center",
    },
    list: {

    },
    listContainer: {
      maxWidth: "100%",
      height: "65%",
      display: "flex",
      alignItems: "center",
      justifyContent: "flex-start",
      margin: 8,
    },
    listTitle: {
      fontSize: largeSize,
      fontWeight: "bold",
      color: theme.colors.text,
    },
    listItem: {
      width: "100%",
    },
    listItemTitle: {
      color: theme.colors.text,
    },
    listItemAvatar: {
      color: theme.colors.text,
      backgroundColor: 'white',

    },
    listItemProgressBar: {
      marginTop: 8,
    },
    listItemBadge: {
    }
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
