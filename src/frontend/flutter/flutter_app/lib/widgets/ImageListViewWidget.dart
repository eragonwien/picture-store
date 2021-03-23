import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_app/services/apiService.dart';

class ImageListView extends StatefulWidget {
  @override
  ImageListViewState createState() => ImageListViewState();
}

class ImageListViewState extends State<ImageListView> {
  String startPage = '';
  final size = 10;
  final folders = <String>[];
  final apiService = new ApiService();

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<Iterable<String>>(
      future: apiService.pageFiles(startPage, size),
      builder: (context, AsyncSnapshot snapshot) {
        if (snapshot.connectionState != ConnectionState.done &&
            !snapshot.hasData) return _buildImageListFolderRow("Loading ...");

        return ListView.builder(
            padding: EdgeInsets.all(0),
            itemBuilder: (context, i) {
              if (i >= (folders.length / 2)) {
                folders.addAll(snapshot.data);

                if (folders.length > 0) startPage = folders.last;
              }

              if (folders.length <= i)
                return _buildImageListFolderRow("Not Found ...");

              return _buildImageListFolderRow(folders[i]);
            });
      },
    );
  }
}

Widget _buildImageListFolderRow(String folder) {
  return ListTile(
    title: Text(folder),
  );
}
