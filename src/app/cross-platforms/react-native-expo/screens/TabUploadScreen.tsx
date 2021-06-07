import { MaterialIcons } from '@expo/vector-icons';
import * as React from "react";
import { Image, StyleSheet } from "react-native";
import { Badge, LinearProgress, ListItem, useTheme } from "react-native-elements";
import { Avatar } from 'react-native-elements/dist/avatar/Avatar';
import { ScrollView } from 'react-native-gesture-handler';
import { ImageUploadView } from "../components/ImageUploadView";
import { View, largeSize, TitleText } from "../components/Themed";

export const TabUploadScreen = ({ }: {}) => {

  const theme = useTheme();

  return (
    <View style={styles.container}>
      <ImageUploadView height={"30%"} />
      <View style={styles.listContainer}>
        <TitleText style={{ padding: 8, textAlign: "left", width: "100%" }}>
          Hochgeladene Bilder
        </TitleText>
        <ScrollView style={{ width: "100%" }}>
          {
            DATA.map((e, i) => (
              <ListItem key={i} bottomDivider>
                <Avatar source={{ uri: e.uri }} imageProps={{ resizeMode: "contain" }} />
                <ListItem.Content>
                  <ListItem.Title>{e.title}</ListItem.Title>
                  <LinearProgress color="primary" value={0.5} variant="determinate" style={{ marginTop: 8 }} />
                </ListItem.Content>
                <MaterialIcons name="check-circle" color="green" size={32} />
              </ListItem>
            ))
          }
        </ScrollView>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
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
    justifyContent: "center",
    margin: 8,
  },
  flatList: {
    width: "100%",
  },
  listTitle: {
    fontSize: largeSize,
    fontWeight: "bold",
  },
  listItem: {
    width: "100%",
    backgroundColor: 'green',
    marginBottom: 8,
    display: "flex",
    flexDirection: "row",
    justifyContent: "flex-start",
    alignItems: "center",
  },
  headingImage: {
    width: 64,
    height: 64,
    margin: 4,
    backgroundColor: 'red'
  },
  listItemStatusBarContainer: {
    width: "100%"
  },
  listITemStatusBar: {
    width: "100%"
  }
});

const DATA = [
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba12",
    title: "First Item",
    progress: "100%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f6311",
    title: "Second Item",
    progress: "50%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d7210",
    title: "Third Item",
    progress: "10%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba9",
    title: "First Item",
    progress: "100%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f638",
    title: "Second Item",
    progress: "50%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d727",
    title: "Third Item",
    progress: "10%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba6",
    title: "First Item",
    progress: "100%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f635",
    title: "Second Item",
    progress: "50%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d724",
    title: "Third Item",
    progress: "10%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba3",
    title: "First Item",
    progress: "100%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f632",
    title: "Second Item",
    progress: "50%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d721",
    title: "Third Item",
    progress: "10%",
    uri: "https://bulma.io/images/bulma-logo.png",
  },
];
