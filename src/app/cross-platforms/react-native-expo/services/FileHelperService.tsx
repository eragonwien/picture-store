import { Platform } from 'react-native'
import * as ImagePicker from 'expo-image-picker';

export class FileHelperService {
    static openImagePickerAsync = async (allowsMultipleSelection: boolean = false, onImagePickedAsync: any) => {
        if (!hasPermissionAsync())
            alert("Permission required");

        const result = await ImagePicker.launchImageLibraryAsync({
            mediaTypes: ImagePicker.MediaTypeOptions.Images,
            allowsEditing: false,
            aspect: [4, 3],
            quality: 1,
            allowsMultipleSelection: true,
        });

        if (onImagePickedAsync && !result.cancelled)
            await onImagePickedAsync();
    }


}
const hasPermissionAsync = async () => {

    if (Platform.OS === "web") return true;

    const { status } = await ImagePicker.requestMediaLibraryPermissionsAsync();

    return status === ImagePicker.PermissionStatus.GRANTED;
}

