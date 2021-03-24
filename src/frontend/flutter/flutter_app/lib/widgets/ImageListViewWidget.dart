import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_app/services/apiService.dart';

class ImageGridViewFutureContainer extends StatefulWidget {
  @override
  ImageFutureWrapperState createState() => ImageFutureWrapperState();
}

class ImageFutureWrapperState extends State<ImageGridViewFutureContainer> {
  final apiService = new ApiService();

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future: apiService.listFiles(),
      builder: (context, AsyncSnapshot<List<String>> snapshot) {
        if (!snapshot.hasData) return Center(child: Text("Loading ..."));

        return buildImageGridView(snapshot.data);
      },
    );
  }
}

Widget buildImageListView(List<String> data) {
  return ListView.builder(
    itemCount: data.length,
    itemBuilder: (BuildContext context, int index) {
      if (data.length <= index) return null;

      return Image.network(data[index]);
    },
  );
}

Widget buildImageGridView(List<String> data) {
  return GridView.builder(
    padding: const EdgeInsets.all(20),
    gridDelegate: SliverGridDelegateWithMaxCrossAxisExtent(
        maxCrossAxisExtent: 200,
        childAspectRatio: 3 / 2,
        crossAxisSpacing: 20,
        mainAxisSpacing: 10),
    itemCount: data.length,
    itemBuilder: (BuildContext context, int index) {
      if (data.length <= index) return null;

      return Container(
        alignment: Alignment.center,
        padding: const EdgeInsets.all(8),
        color: Colors.teal[300],
        child: Image.network(data[index]),
      );
    },
  );
}
