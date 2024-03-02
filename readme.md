# 火柴人vsCAD

This game is a Project for 2024 THU software engineering contest.💻

Uses Unity to develop a 2D game of Stick Figures and CAD.💾

Credits: TUOMF, Xyberoid, KnightNemo🧑‍💻

## Overview

* 场景中包含了**玩家**，**鼠标光标**和很多**物体**和**敌对单位**🤖

* 玩家有四个技能可以使用:
    *  左键发射**删除**(攻击)🖊️
    * 按X准备**传送**(再按一次取消准备),再按空格移动到鼠标的位置;🎃
    * 按E准备**分解**(再按一次取消准备)，再按空格会激活`Tag=="Array"`的物体掉落;👾
    * 按F投掷**圆角化工具**，可以把尖刺(`spike`)变钝使之无害👻

* 技能的释放部分都在`playercontroller`脚本里，都有对应的函数(`Teleport Erase Breakdown`)，删除和圆角有各自的计时器⏱️

* 玩家是单例，`playercontroller.instance.`可以在任何位置拿到他`public`的东西🏃

* 技能希望加上一个获取后才能使用的设置，技能图标，解锁情况和使用次数在UI显示🔐

* 其他的组件暂时不用做什么调整🤗

## 0219更新:
* 增加了有HP和技能的UI/复活🗿

* **HP机制**:显示数字和相对应的六边形数(用的不是预制件)❤️

* **技能机制**:
    * 未解锁前显示为叉和"Locked"，解锁后叉消失，显示"x/n"，n为可使用总次数，在`playercontroller`的`chances[]`数组里,`x==0`时图标蒙灰，且无法再使用。
    * 目前n暂时默认为5，在`eraseChance`等变量中修改。
    * 使用技能事会出现提示框(预制件)，3个以上或4s以上自动消失(在`Console0Script`和`TipsScript`)里
* 死后点击右上角按钮本关重新开始✝️

## 0220更新:
* `DontDestroyOnLoad`仅包括`LogicManagers`和`Canvas0`，其他需要共用的每个场景单独放(包括`Camera`和`player`相关的)，`player`的数据可以放到`LogicManagers`里的`Logic`的脚本`LogicScript`中。🤯
已实例化可以LogicScript.instance引用

## 0221更新:
* 增加了**记录点**(文件保存的图标)，玩家碰到后消失并将这个位置作为重生点。在实际关卡设置中应避免玩家回到上一个重生点。
* 改进了**分解**技能，如果绿色的探测区域没有接触到阵列物体(蓝色区域不会产生)，则不会消耗使用次数。
## 0302更新:
* 完成小boss: `Terminal`的制作,`Terminal`在玩家进入特定范围后进入`ActiveState`,能发射6种不同的炮弹:
    * `rm`:发射正常的炮弹，击中玩家后玩家`HP`-1🚀
    * `:wq`:发射的炮弹会短时间内禁用玩家的所有技能🈲
    * `rm-f`:发射致死的炮弹，提前会有瞄准射线提示🔔
    * `tac`:击中玩家后让玩家倒立，此时可以使用技能、左右移动,但不能跳跃
    * `--help`召唤小兵,仍在开发中🔧
    * `move`:将玩家随机传送到与当前位置$\Delta y\in (0,10)$,$\Delta x\in (-5,5)$的位置