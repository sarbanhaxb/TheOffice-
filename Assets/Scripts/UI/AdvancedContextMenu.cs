using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class AdvancedContextMenu : MonoBehaviour
{
    private VisualElement root;
    private Label targetLabel;

    void OnEnable()
    {
        // �������� �������� �������
        root = GetComponent<UIDocument>().rootVisualElement;

        // ������� ������� �������, �� ������� ����� ����������� ����
        targetLabel = new Label("Right-click here for context menu");
        targetLabel.style.marginTop = 50;
        targetLabel.style.marginLeft = 50;
        targetLabel.style.fontSize = 20;
        targetLabel.style.color = Color.white;
        targetLabel.style.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
        targetLabel.style.paddingTop = 10;
        targetLabel.style.paddingBottom = 10;
        targetLabel.style.paddingLeft = 20;
        targetLabel.style.paddingRight = 20;

        root.Add(targetLabel);

        // ������� ����������� ������������ ����
        var menuManipulator = new ContextualMenuManipulator(FillContextMenu);

        // ������������� ����
        menuManipulator.target = targetLabel;

        // ������������� ����� ������������ ����������� ������� �������
        // targetLabel.RegisterCallback<ContextualMenuPopulateEvent>(OnContextMenuPopulate);
    }

    void FillContextMenu(ContextualMenuPopulateEvent evt)
    {
        // ��������� ������� �������
        evt.menu.AppendAction("Copy",
            action => Debug.Log("Copy action triggered"),
            DropdownMenuAction.AlwaysEnabled);

        // ��������� ������� � ��������� ���������
        evt.menu.AppendAction("Paste",
            action => Debug.Log("Paste action triggered"),
            CanPaste ? DropdownMenuAction.AlwaysEnabled : DropdownMenuAction.AlwaysDisabled);

        // ��������� �����������
        evt.menu.AppendSeparator();

        // ��������� ��������� ����
        evt.menu.AppendAction("Advanced/Option 1",
            action => Debug.Log("Advanced option 1"));
        evt.menu.AppendAction("Advanced/Option 2",
            action => Debug.Log("Advanced option 2"));

        // ����������� ���������� �������
        evt.menu.AppendAction($"Current Time: {System.DateTime.Now:T}",
            action => { },
            DropdownMenuAction.AlwaysDisabled);
    }

    bool CanPaste => GUIUtility.systemCopyBuffer.Length > 0;

    // �������������� ������ � �������������� ������� ContextualMenuPopulateEvent
    void OnContextMenuPopulate(ContextualMenuPopulateEvent evt)
    {
        evt.menu.AppendAction("Custom Action", action => {
            Debug.Log($"Action on {((Label)evt.target).text}");
        });
    }
}