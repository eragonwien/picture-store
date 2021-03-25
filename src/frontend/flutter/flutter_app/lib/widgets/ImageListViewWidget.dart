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

Widget buildImageGridView(List<String> data) {
  return GridView.builder(
    padding: const EdgeInsets.all(20),
    gridDelegate: SliverGridDelegateWithMaxCrossAxisExtent(
        maxCrossAxisExtent: 500.0, crossAxisSpacing: 20, mainAxisSpacing: 20),
    itemCount: data.length,
    itemBuilder: (BuildContext context, int index) {
      if (data.length <= index) return null;

      return Container(
        alignment: Alignment.center,
        padding: const EdgeInsets.all(8),
        child: buildImageGridViewCardItem(data[index]),
      );
    },
  );
}

Widget buildImageGridViewCardItem(String data) {
  return Center(
    child: Card(
      elevation: 10,
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          buildImageGridViewCardItemHeader(),
          buildImageGridViewCardItemImage(data),
          buildImageGridViewCardItemActions(),
        ],
      ),
    ),
  );
}

Widget buildImageGridViewCardItemHeader() {
  return ListTile(
    leading: Icon(Icons.album),
    title: Text('Flutter Card Image Example'),
    subtitle: Text('An example flutter card with image'),
  );
}

Widget buildImageGridViewCardItemImage(String data) {
  return Expanded(
    child: Image.network(data, fit: BoxFit.scaleDown),
  );
}

Widget buildImageGridViewCardItemActions() {
  return ButtonBar(
    children: [
      ElevatedButton(child: new Text('Like'), onPressed: null),
      ElevatedButton(child: new Text('Like'), onPressed: null),
    ],
  );
}
