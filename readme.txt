场景中包含了玩家，鼠标光标和很多物体和敌对单位

玩家有四个技能可以使用，左键发射删除（攻击）；按x准备传送（再按一次取消准备），再按空格移动到鼠标的位置；按E准备分解（再按一次取消准备），再按空格会激活Tag=="Array"的物体掉落；按F投掷圆角化工具，可以把尖刺（spike）变钝使之无害

技能的释放部分都在playercontroller脚本里，都有对应的函数（Teleport Erase Breakdown），删除和圆角有各自的计时器

玩家是单例，playercontroller.instance.可以在任何位置拿到他public的东西

技能希望加上一个获取后才能使用的设置，技能图标，解锁情况和使用次数在UI显示

其他的组件暂时不用做什么调整

0219更新：
增加了有HP和技能的UI\复活

HP机制：显示数字和相对应的六边形数（用的不是预制件）

技能机制：未解锁前显示为叉和"Locked"，解锁后叉消失，显示"x/n"，n为可使用总次数，在playercontroller的chances[]数组里,x==0时图标蒙灰，且无法再使用。
目前n暂时默认为5，在eraseChances等变量中修改。
使用技能事会出现提示框（预制件），3个以上或4s以上自动消失（在Console0Script和TipsScript）里

死后点击右上角按钮本关重新开始

0220更新：
DontDestroyOnLoad仅包括LogicManagers和Canvas0，其他需要共用的每个场景单独放（包括Camera和player相关的），player的数据可以放到LogicManagers里的Logic的脚本LogicScript中。
已实例化可以LogicScript.instance引用

0221更新：
增加了记录点（文件保存的图标），玩家碰到后消失并将这个位置作为重生点。在实际关卡设置中应避免玩家回到上一个重生点。
改进了分解技能，如果绿色的探测区域没有接触到阵列物体（蓝色区域不会产生），则不会消耗使用次数。