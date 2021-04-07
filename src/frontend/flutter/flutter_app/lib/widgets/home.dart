import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_app/services/api_service.dart';
import 'package:flutter_app/widgets/features/image_upload/image_upload_view.dart';
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
        ImageUploadPage(),
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
    setState(() {
      _selectedIndex = index;

      switch (index) {
        case 1:
          _title = 'Uploads';
          break;
        case 2:
          _title = 'Settings';
          break;
        default:
          _title = widget.title;
          break;
      }
    });
  }
}
