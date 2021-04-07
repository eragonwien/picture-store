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
  int _selectedIndex = 0;
  late String _title;

  @override
  void initState() {
    _title = widget.title;
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        backgroundColor: Colors.white,
        appBar: buildAppBar(),
        body: buildNavigationPage(),
        bottomNavigationBar: buildBottomNavigationBar(),
      ),
    );
  }

  AppBar buildAppBar() {
    return AppBar(
      title: Text(_title),
    );
  }

  Widget buildNavigationPage() {
    return IndexedStack(
      index: _selectedIndex,
      children: <Widget>[
        ImageGridViewsContainer(),
        Container(),
        Container(),
      ],
    );
  }

  Widget buildBottomNavigationBar() {
    return BottomNavigationBar(
      currentIndex: _selectedIndex,
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

  void onItemTapped(int index) async {
    if (index == 1) await uploadImage();

    setState(() {
      switch (index) {
        case 0:
          _title = widget.title;
          _selectedIndex = index;
          break;
        case 2:
          _title = 'Settings';
          _selectedIndex = index;
          break;
        default:
          _title = widget.title;
          break;
      }
    });
  }

  Future uploadImage() async {
    final pickedFile = await imagePicker.getImage(source: ImageSource.gallery);

    if (pickedFile == null) return;

    await apiService.uploadAsync(pickedFile.path);
  }
}
