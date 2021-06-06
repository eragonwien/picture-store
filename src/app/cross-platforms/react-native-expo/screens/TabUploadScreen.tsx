import { MaterialIcons } from "@expo/vector-icons";
import * as React from "react";
import { StyleSheet } from "react-native";
import { FlatList } from "react-native-gesture-handler";
import { IconButton } from "../components/IconButton";
import { ImageUploadView } from "../components/ImageUploadView";
import { View, Text, largeSize, TitleText } from "../components/Themed";

export const TabUploadScreen = ({}: {}) => {
  const renderItem = ({ item }: { item: any }) => {
    return (
      <View style={styles.listItem}>
        <View
          style={{
            width: "100%",
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            justifyContent: "space-between",
          }}
        >
          <View>
            <Text>wird hochgeladen</Text>
            <Text>68% - 12 Minuten verbleiben</Text>
          </View>
          <View style={{ display: "flex", flexDirection: "row" }}>
            <IconButton
              icon={<MaterialIcons name="speaker" size={32} color="black" />}
            />
            <IconButton
              icon={<MaterialIcons name="video-call" size={32} color="black" />}
            />
          </View>
        </View>
      </View>
    );
  };

  return (
    <View style={styles.container}>
      <ImageUploadView height={"30%"} />
      <View style={styles.listContainer}>
        <TitleText style={{ padding: 8, textAlign: "left", width: "100%" }}>
          Hochgeladene Bilder
        </TitleText>
        <FlatList
          data={DATA}
          renderItem={renderItem}
          keyExtractor={(item) => item.id}
        />
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
  listTitle: {
    fontSize: largeSize,
    fontWeight: "bold",
  },
  listItem: {
    maxWidth: "100%",
    height: 64,
    display: "flex",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    borderWidth: 4,
    borderRadius: 16,
    shadowColor: "black",
    shadowOpacity: 50,
    padding: 8,
    marginBottom: 8,
  },
});

const DATA = [
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba12",
    title: "First Item",
    progress: "100%",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f6311",
    title: "Second Item",
    progress: "50%",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d7210",
    title: "Third Item",
    progress: "10%",
  },
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba9",
    title: "First Item",
    progress: "100%",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f638",
    title: "Second Item",
    progress: "50%",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d727",
    title: "Third Item",
    progress: "10%",
  },
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba6",
    title: "First Item",
    progress: "100%",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f635",
    title: "Second Item",
    progress: "50%",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d724",
    title: "Third Item",
    progress: "10%",
  },
  {
    id: "bd7acbea-c1b1-46c2-aed5-3ad53abb28ba3",
    title: "First Item",
    progress: "100%",
  },
  {
    id: "3ac68afc-c605-48d3-a4f8-fbd91aa97f632",
    title: "Second Item",
    progress: "50%",
  },
  {
    id: "58694a0f-3da1-471f-bd96-145571e29d721",
    title: "Third Item",
    progress: "10%",
  },
];
