   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   B >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1 5   	   Z VHl7.Fhir.STU3.Core, Version=0.96.0.0, Culture=neutral, PublicKeyToken=d706911480550fc3K8clr-namespace:Hl7.Fhir.Model;assembly=Hl7.Fhir.STU3.CoreHl7.Fhir.Model 5	   	   5clr-namespace:WpfApp1.ViewModelWpfApp1.ViewModel 5
   	      WpfApp1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   'localclr-namespace:WpfApp1     5   	   R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5   	   EModels8clr-namespace:Hl7.Fhir.Model;assembly=Hl7.Fhir.STU3.Core  5	   	   0
viewmodelsclr-namespace:WpfApp1.ViewModel  5
   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5         dataFromEnum S� 
MethodName S� 
ObjectType   System.Enum S� MethodParameters M� TypeName��%�   &	        S� 5   
   $ 	GetValues��62   "  5       5      M� 5      $! Models:AdministrativeGender��6   5      5      .��5      ]�  � DataContext 5   
   , &WpfApp1.ViewModel.PatientListViewModel  5      .+�5   
   �� $	��Top=�6   ��6)   �� *FetchedPatientsNumber+ �� StringFormat$ Znaleziono {0} pacjentów���� 5   
   -   5         ��Name$ ListOfPatients��5      $
��Left=�6*   $	��380��6@   $��Visible=�6q   ��6P   �� *
Patients+	 �� SelectedItem	 6�   �� *SelectedPatient+��5      �� 5      .R�5      �� $��0, 0, 0, 12q�6!   .+�5      �� ��6$   �� *Id+�� 5      ��6$   �� *Name+5       5!      5#      �� 5$   
   $��Bottom=�6   
 �� Orientation$
 
Horizontal=�6-   .+�5%      �� ��6   �� *FilterDataCommand+.��6;   Filter6C   �� 5&      $	��100��6   ��6%   �� *FilterValue+6E   5'      �� 5(   
    6   �� *SelectedPatient+.@�5)      �� $��Details��6   .��5*      � -   5        $��PatientDetailsList��5*       � RowDefinitions 5+      �� 5,      $
��Auto �6(   �� 5-      $��* �6(   .+�5/      � $��0��6    � ColumnDefinitions 50      �� 51      $
��Auto �6/   6>   �� 52      $��* �6/   6;    54      �� 55      6.   �� 56      6.   �� 57      6.   �� 58      6.   �� 59      6.   .+�5;      �� $��0��6    .��6-   ID:62   �� 5<      $��0��6"   $��1��6/    �� 
IsReadOnly . $	��150��6Q   ��6`   �� *Id+ �� Mode$ OneWay=��� 5=      $��1��6    .��6-   Name:64   �� 5>      $��1��6"   $��1��6/    . $	��150��6Q   ��6`   �� *Name+$ OneWay=��� 5?      $��2��6    .��6-   Date of birth:6=   �� 5@      $��2��6"   $��1��6/    . $	��150��6Q   ��6`   �� *	BirthDate+$ OneWay=��� 5A      $��3��6    .��6-   	Gender:66   �� 5B      $��3��6"   $��1��6/    . ��6Q   �� *Gender+$ OneWay=��� 5C      $��4��6    .��6-   
Address:67   �� 5D      $��4��6"   $��1��6/    . ��6Q   �� *	Address+$ OneWay=�5E      5F      5G      �� 5H      $��History��6   .��5I      ]� .+�5J      �� -   5        $ HistoryListing��5J      $��Visible=�6Z   ��66   �� *HistoryList+��5K      �� 5L      .R�5M   "   �  5N   &   �� 5O   *    5Q   &   �� 5R   *   $
��Auto �6;   �� 5S   *   $
��Auto �6;   �� 5T   *   $
��Auto �6;   �� 5U   *   $
��Auto �6;   �� 5V   *   $
��Auto �6;   .+�5X   &   � $��0��6+   .+�5Y   *   A� $��20��62   $��20��6=   
�������
����+��$��2��6n   �� 5Z   *   .��60   !63   5[   '   �� 5\   &   $��1��60   $��0 0 5 0q�6`   ��6@   ��  �� Path$ 	Effective�6`   �� 5]   &   $��2��60   $��0 0 5 0q�6_   ��6@   �� $ TypeName�6_   �� 5^   &   $��3��60   $��0 0 5 0q�6`   ��6@   �� $ 	Code.Text�6`   5_   #   5`      5b      �� 5c      $��Bottom=�6   ��66   �� *HistoryCount+$ Pobrano {0} zdarzeń��5d      5e      �� 5f      $��Wizualizacja��6   .��5g      �  5h      �� 5i      $��2* �6+   �� 5j      $��* �6+   .+�5l        OxyPlot.Wpf.Plot     Title 6   �� *ChosenValue+   Series 5m        OxyPlot.Wpf.LineSeries  5n      ��6-   �� *PlotDataSet+5p      � 5q      $��1��6    5r      �� 5s      $
��Auto �6,   .+�5u      �  5v      �� 5w   "   �� 5x   "   �� 5y   "   �� 5z   "    5|      �� 5}   "   $
��Auto �63   �� 5~   "   $
��Auto �63   .+�5�      �� $��0��6$   $��0��64   .��6B   
Pomiary:6L   �� 5�      -   5         ��Name$ 	Selection��5�   '   $��1��6�   $��0��6�   ��6:   �� *PossibleValues+	 6a   �� *ChosenValue+6�   �� 5�      $��0��6$   $��1��64   .��6B   Start:6J   �� 5�      -   5        $ Start��5�   '    �� DisplayMemberPath$ 	Effective��6X   $��1��6�   $��1��6�   ��66   �� *	ChosenSet+	 6v   �� *Start+6�   �� 5�      $��0��6$   $��2��64   .��6B   	Koniec:6K   �� 5�      -   5        $	 End��5�   '   $ 	Effective��6V   $��1��6�   $��2��6�   ��64   �� *	ChosenSet+	 6t   �� *End+6�   �� 5�      $��3��6%   ��62   �� *PlotCommand+.��6R   Apply6Y   5�      5�      5�      5�      5�      5�      5�      