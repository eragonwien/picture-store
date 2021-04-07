import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_app/services/api_service.dart';
import 'package:flutter_app/widgets/features/images_grid_view/image_grid_view.dart';
import 'package:image_picker/image_picker.dart';

class HomePage extends StatefulWidget {
  HomePage({Key? key, required this.title}) : super(key: key);

  final String title;

  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  final apiService = ApiService();
  final imagePicker = ImagePicker();
  int selectedIndex = 0;

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        backgroundColor: Colors.white,
        appBar: AppBar(
          title: Text(widget.title),
        ),
        body: buildNavigationPage(),
        bottomNavigationBar: buildBottomNavigationBar(),
      ),
    );
  }

  Widget buildNavigationPage() {
    return IndexedStack(
      index: selectedIndex,
      children: <Widget>[
        ImageGridViewsContainer(),
        Container(),
        Container(),
      ],
    );
  }

  Widget buildBottomNavigationBar() {
    return BottomNavigationBar(
      currentIndex: selectedIndex,
      items: const <BottomNavigationBarItem>[
        BottomNavigationBarItem(
          icon: Icon(Icons.grid_view),
          label: 'Photos',
          backgroundColor: Colors.red,
        ),
        BottomNavigationBarItem(
          icon: Icon(Icons.add),
          label: 'Upload',
          backgroundColor: Colors.red,
        ),
        BottomNavigationBarItem(
          icon: Icon(Icons.settings),
          label: 'Settings',
          backgroundColor: Colors.red,
        ),
      ],
      onTap: onItemTapped,
    );
  }

  void setPage(int index) {
    setState(() {
      selectedIndex = index;
    });
  }

  void onItemTapped(int index) async {
    switch (index) {
      case 1:
        await uploadImage();
        break;
      default:
        setPage(index);
        break;
    }
  }

  Future uploadImage() async {
    final pickedFile = await imagePicker.getImage(source: ImageSource.gallery);

    if (pickedFile == null) return;

    await apiService.uploadAsync(pickedFile.path);
  }
}
