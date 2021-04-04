import 'package:flutter/material.dart';
import 'package:flutter_app/services/api_service.dart';
import 'package:pinch_zoom/pinch_zoom.dart';
import 'package:transparent_image/transparent_image.dart';

class ImageDialog extends StatefulWidget {
  final String image;

  const ImageDialog({Key? key, required this.image}) : super(key: key);

  @override
  ImageDialogState createState() => ImageDialogState(this.image);
}

class ImageDialogState extends State<ImageDialog> {
  String image;
  int currentIndex = 0;
  final apiService = new ApiService();

  ImageDialogState(this.image) {
    this.image = image;
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: AppBar(),
        body: Container(
          child: PinchZoom(
            image: FadeInImage.memoryNetwork(
              placeholder: kTransparentImage,
              image: image,
            ),
            zoomedBackgroundColor: Colors.black.withOpacity(0.5),
            resetDuration: const Duration(milliseconds: 100),
            maxScale: 2.5,
          ),
        ),
        floatingActionButton: buildFloatingActionButton(),
      ),
    );
  }

  Widget buildFloatingActionButton() {
    return FloatingActionButton(
      onPressed: onFloatingActionButtonPressed,
      child: Icon(Icons.delete),
      backgroundColor: Colors.redAccent,
    );
  }

  void onFloatingActionButtonPressed() {
    showDialog(
        context: context,
        barrierDismissible: false,
        builder: (BuildContext context) {
          return AlertDialog(
            actionsPadding: EdgeInsets.only(right: 5),
            content: Container(
              child: Text("Delete this ?"),
            ),
            actions: <Widget>[
              TextButton(
                child: Text('Confirm'),
                style: TextButton.styleFrom(
                    primary: Colors.red, backgroundColor: Colors.white),
                onPressed: onConfirmationButtonPressed,
              ),
              TextButton(
                child: Text('Cancel'),
                onPressed: onCancelButtonPressed,
              ),
            ],
          );
        }).then((value) => onDeleteImageConfirmationDialogClosed(value));
  }

  void onConfirmationButtonPressed() {
    apiService.deleteFile(this.image).then((value) => onImageDeleted());
  }

  void onImageDeleted() {
    ScaffoldMessenger.of(context)
        .showSnackBar(SnackBar(content: Text("Image deleted")));

    Navigator.of(context).pop(true);
  }

  void onCancelButtonPressed() {
    Navigator.of(context).pop(false);
  }

  void onDeleteImageConfirmationDialogClosed(bool isDeleted) {
    if (isDeleted) {
      Navigator.of(context).pop(isDeleted);
    }
  }
}
