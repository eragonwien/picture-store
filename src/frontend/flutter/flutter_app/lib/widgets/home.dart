import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class HomePage extends StatefulWidget {
  HomePage({Key key, this.title}) : super(key: key);

  final String title;

  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Center(
        child: ImageListView(),
      ),
    );
  }
}

class ImageListView extends StatefulWidget {
  @override
  _ImageListViewState createState() => _ImageListViewState();
}

class _ImageListViewState extends State<ImageListView> {
  final folders = <String>[];

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      padding: EdgeInsets.all(0),
      itemBuilder: (context, i){
        if (i >= (folders.length/2)) {
          folders.addAll(['1', '2', '3', '4', '5', '6', '7', '8', '9', '10']);
        }

        return _buildImageListFolderRow(folders[i]);
      });
  }
}

Widget _buildImageListFolderRow(String folder) {
  return ListTile(
    title: Text(folder),
  );
}