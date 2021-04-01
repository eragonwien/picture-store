import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_app/services/apiService.dart';
import 'package:flutter_app/widgets/modals/ImageModal.dart';
import 'package:transparent_image/transparent_image.dart';

class ImageGridViewsContainer extends StatefulWidget {
  @override
  _ImageGridViewsContainerState createState() =>
      _ImageGridViewsContainerState();
}

class _ImageGridViewsContainerState extends State<ImageGridViewsContainer> {
  final apiService = new ApiService();

  List<String> images = [];
  String? selectedImage;

  @override
  Widget build(BuildContext context) {
    print('ImageGridViewsContainer is built');
    return Container(
      child: FutureBuilder(
        future: apiService.listFiles(),
        builder: (BuildContext context, AsyncSnapshot<List<String>> snapshot) {
          if (snapshot.hasError) {
            ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(content: Text(snapshot.error.toString())));
            return Container();
          }

          if (!snapshot.hasData)
            return Center(child: CircularProgressIndicator());

          images = snapshot.data!.toList();
          final orientation = MediaQuery.of(context).orientation;

          return buildGridView(images, orientation);
        },
      ),
    );
  }

  Widget buildGridView(List<String> data, Orientation orientation) {
    return Container(
      margin: EdgeInsets.all(12),
      child: GridView.builder(
        gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
            crossAxisCount: orientation == Orientation.portrait ? 2 : 3,
            crossAxisSpacing: 10,
            mainAxisSpacing: 10),
        itemCount: data.length,
        itemBuilder: (BuildContext context, int index) {
          if (data.length <= index) return Container();

          String image = data[index];

          return Container(
            child: ClipRRect(
              borderRadius: BorderRadius.circular(15),
              child: GestureDetector(
                onTap: () => onImageTileTapped(context, image),
                child: FadeInImage.memoryNetwork(
                  image: image,
                  placeholder: kTransparentImage,
                  fit: BoxFit.cover,
                ),
              ),
            ),
            decoration: BoxDecoration(
                color: Colors.transparent,
                border: Border.all(color: Colors.orange),
                borderRadius: BorderRadius.all(Radius.circular(15))),
          );
        },
      ),
    );
  }

  onImageTileTapped(BuildContext context, String image) {
    selectedImage = image;
    Navigator.of(context)
        .push(
          MaterialPageRoute(
              builder: (BuildContext context) {
                return ImageDialog(image: image);
              },
              fullscreenDialog: true),
        )
        .then((value) => onImageDialogClosed(value));
  }

  void onImageDialogClosed(value) {
    bool isDeleted = value != null && value as bool;

    if (!isDeleted) return;

    triggerStateChange();

    selectedImage = null;
  }

  void triggerStateChange() {
    setState(() {});
  }
}