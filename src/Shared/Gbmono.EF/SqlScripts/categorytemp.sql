CREATE table CategoryTemp
 (  
  CategoryId int identity not null, 
  CategoryCode varchar(2) not null, 
  ParentId int null,
  CategoryName nvarchar(100) not null
 )

insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 1,'01',NULL,'医药品?医药部外品イヤクヒンイヤクブガイヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 2,'01',1,'饮料')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 3,'01',2,'恢复疲劳类饮料ヒロウカイフク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 4,'02',2,'美容饮料ビヨウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 5,'02',1,'维他命')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 6,'01',5,'维他命剂（固体?粉粉?颗粒）ザイコケイコナサイリュウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 7,'03',1,'感冒カゼ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 8,'01',7,'解热镇痛药ゲネツチンツウザイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 9,'02',7,'综合感冒药ソウゴウクスリ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 10,'03',7,'止咳セキド')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 11,'04',1,'过敏')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 12,'01',11,'各种过敏?鼻炎药ヨウザイビエンクスリ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 13,'05',1,'肠胃药イチョウヤク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 14,'01',13,'肠胃药イチョウヤク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 15,'02',13,'整肠药セイチョウザイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 16,'03',13,'止泻药シシャヤク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 17,'06',1,'眼药水メグスリ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 18,'01',17,'眼药メグスリ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 19,'02',17,'洗眼药センガンヤク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 20,'07',1,'外用药ガイヨウヤク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 21,'01',20,'外用消炎镇痛剂ガイヨウショウエンチンツウザイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 22,'02',20,'皮肤疾患用药ヒフシッカンヨウヤク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 23,'03',20,'痔疮用药ジシッカンヨウヤク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 24,'08',1,'育发イクモウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 25,'01',24,'育发剂イクモウザイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 26,'09',1,'女性?汉方フジンカンポウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 27,'01',26,'女性用药フジンヨウヤク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 28,'02',26,'汉方药カンポウヤク')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 29,'10',1,'其他タ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 30,'01',29,'其他医药品?医药部外品ホカイヤクヒンイヤクブガイヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 31,'02',NULL,'医疗用具イリョウヨウグ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 32,'01',31,'口罩?伤口贴バンソウコウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 33,'01',32,'感冒护理用品ヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 34,'02',32,'伤口护理用品ヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 35,'02',31,'肩颈椎护理カタ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 36,'01',35,'辅助?束身类')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 37,'02',35,'肩膀酸痛护理用品ヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 38,'03',31,'仪器ケイキ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 39,'01',38,'测定器具ソクテイキキ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 40,'04',31,'スキン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 41,'01',40,'受胎调整用品ジュタイチョウセイヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 42,'05',31,'其他タ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 43,'01',42,'其他医疗用具ホカイリョウヨウグ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 44,'03',NULL,'健康食品ケンコウショクヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 45,'01',44,'减肥')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 46,'01',45,'减肥')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 47,'02',45,'酵素コウソ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 48,'03',45,'美容ビヨウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 49,'02',44,'维他命')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 50,'01',49,'维他命?矿物质')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 51,'03',44,'氨基酸サン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 52,'01',51,'氨基酸?蛋白质サン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 53,'02',51,'青汁?螺旋藻?小球藻アオジル')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 54,'04',44,'功能食品キノウショクヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 55,'01',54,'肝脏护理（饮酒辅助）キモインシュ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 56,'02',54,'DHA?EPA')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 57,'03',54,'软骨素?葡萄糖胺')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 58,'04',54,'眼睛辅助ヒトミ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 59,'05',44,'茶?提取物チャ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 60,'01',59,'健康茶?健康醋ケンコウチャケンコウス')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 61,'02',59,'提取剂ザイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 62,'06',44,'他タ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 63,'01',62,'其他健康食品ホカケンコウショクヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 64,'04',NULL,'基础化妆品キソケショウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 65,'01',64,'基础化妆类キソケショウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 66,'01',65,'洁面センガンリョウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 67,'02',65,'卸妆')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 68,'03',65,'化妆水ケショウスイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 69,'04',65,'乳液?美容液ニュウエキビヨウエキ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 70,'02',64,'面膜')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 71,'01',70,'面膜')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 72,'02',70,'局部护理?特殊护理')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 73,'03',70,'其他护肤ホカ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 74,'05',NULL,'打底妆')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 75,'01',74,'打底妆')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 76,'01',75,'粉底')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 77,'02',75,'打底霜?遮瑕膏シタジ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 78,'03',75,'散粉')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 79,'04',75,'其他打底妆ホカ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 80,'06',NULL,'局部妆')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 81,'01',80,'眼部ヒトミ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 82,'01',81,'眉刷')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 83,'02',81,'眼线')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 84,'03',81,'睫毛膏')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 85,'04',81,'眼影')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 86,'02',80,'脸部カオ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 87,'01',86,'口红?唇彩クチベニ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 88,'02',86,'腮红')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 89,'03',86,'指甲油?美甲')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 90,'04',86,'其他局部妆ホカ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 91,'07',NULL,'一般化妆品イッパンケショウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 92,'01',91,'护理')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 93,'01',92,'身体护理')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 94,'02',92,'手霜')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 95,'03',92,'护唇膏')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 96,'02',91,'防晒')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 97,'01',96,'防晒（脸部）カオヨウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 98,'02',96,'防晒（身体）ヨウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 99,'03',91,'礼仪（外观）')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 100,'01',99,'礼仪（外观）用品')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 101,'02',99,'除毛?脱毛ジョモウダツモウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 102,'03',99,'腿部护理')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 103,'04',99,'连裤袜?功能性连裤袜キノウセイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 104,'05',99,'化妆用品ケショウコモノ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 105,'04',91,'美容电器ビヨウカデン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 106,'01',105,'美容家电ビヨウカデン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 107,'05',91,'香水コウスイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 108,'01',107,'香水')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 109,'06',91,'其他タ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 110,'01',109,'其他化妆品ホカケショウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 111,'08',NULL,'头发护理')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 112,'01',111,'洗发水')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 113,'01',112,'洗发水?护发素')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 114,'02',111,'护理')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 115,'01',114,'护发乳?护发膜')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 116,'02',114,'修复?免洗护发类ホシュウザイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 117,'03',111,'头皮')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 118,'01',117,'头皮护理?育发イクモウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 119,'09',NULL,'染发')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 120,'01',119,'染发')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 121,'01',120,'时尚染发ゾメ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 122,'02',120,'染白发シラガソメ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 123,'03',120,'烫发液エキ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 124,'10',NULL,'头发造型')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 125,'01',124,'头发造型')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 126,'01',125,'整型ネナオ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 127,'02',125,'定型喷雾胶?喷发水')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 128,'03',125,'发蜡?发油')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 129,'04',125,'啫喱水')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 130,'05',125,'定型摩丝')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 131,'06',125,'其他发型用品ホカ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 132,'11',NULL,'沐浴?肥皂ニュウヨクセッケン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 133,'01',132,'液体エキタイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 134,'01',133,'沐浴露')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 135,'02',132,'固体コタイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 136,'01',135,'固体肥皂コケイセッケン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 137,'03',132,'手霜')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 138,'01',137,'洗手液')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 139,'04',132,'泡浴剂ニュウヨクザイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 140,'01',139,'泡浴剂（盐）ニュウヨクザイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 141,'12',NULL,'口腔护理')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 142,'01',141,'牙膏ハミガ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 143,'01',142,'牙膏')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 144,'02',141,'牙刷ハ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 145,'01',144,'牙刷?牙缝刷')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 146,'02',144,'电动牙刷デンドウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 147,'03',141,'口腔护理')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 148,'01',147,'漱口水')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 149,'02',147,'口臭护理コウシュウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 150,'04',141,'幼儿コドモ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 151,'01',150,'幼儿牙膏コドモ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 152,'02',150,'幼儿牙刷コドモ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 153,'05',141,'其他タ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 154,'01',153,'其他口腔护理ホカ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 155,'13',NULL,'女性用品ジョセイヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 156,'01',155,'纸类')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 157,'01',156,'卫生巾セイリヨウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 158,'02',155,'棉条')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 159,'01',158,'生理用棉条セイリヨウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 160,'03',155,'护垫')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 161,'01',160,'生理用护垫')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 162,'04',155,'其他タ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 163,'01',162,'轻微失禁ケイシッキン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 164,'02',162,'其他女性用品ホカジョセイヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 165,'14',NULL,'婴童用品ヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 166,'01',165,'纸尿布カミ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 167,'01',166,'纸尿裤カミ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 168,'02',166,'纸尿裤（拉拉裤）カミ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 169,'02',165,'食品')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 170,'01',169,'婴儿奶粉')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 171,'02',169,'婴儿食品')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 172,'03',165,'日用品ヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 173,'01',172,'婴儿湿巾纸アカヨウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 174,'02',172,'婴儿用品ヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 175,'15',NULL,'男性化妆品ダンセイケショウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 176,'01',175,'男性化妆品ケショウ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 177,'01',176,'洁面センガン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 178,'02',176,'基础化妆品キソケショウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 179,'03',176,'礼仪（外观）用品')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 180,'02',175,'剃须刀')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 181,'01',180,'男性剃须刀ダンセイ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 182,'02',180,'剃须用品ヨウヒン')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 183,'03',175,'男性头发护理')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 184,'01',183,'洗发水?护发素')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 185,'02',183,'头皮护理?育发')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 186,'03',183,'发型用品')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 187,'04',175,'男性其他タ')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 188,'01',187,'防晒')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 189,'02',187,'护唇膏')
insert categorytemp (CategoryId,CategoryCode,ParentId,CategoryName)  values ( 190,'03',187,'其他男性化妆品ホカダンセイケショウヒン')


  CREATE table ProductTemp
 (  
    productid int identity,
    productCode varchar(max) null,
	categoryid int null,
	BrandId int null,
	PromotionCode varchar(max) null,
	CuponCode varchar(max) null,
	topicId int null,
	primaryname varchar(max) null,
	secondaryname varchar(max) null,
	flavor varchar(max) null,
	content varchar(max) null,
	displaycode varchar(max) null,
	barcode varchar(max) null,
	price float null,
	[description] varchar(max) null,
	instruction varchar null,
	extrainformation varchar null
)

insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 1,'0001',18,1,'7001','0000',NULL,'アース製薬 ダニアースレッド ダニ・ノミ用 6~8畳用 10g×3【第2類医薬品】',NULL,'','10g?3','0000','4901080414427','1,522円（税込）','目に見えないダニの駆除＆予防水を使うから 少ない煙でよく効く！ お部屋を汚さない！ペットのいるご家庭にも！...','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約22mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。
','＜使用量の目安＞
各害虫の駆除には次の使用量をお守りください。
○屋内塵性ダニ類の増殖抑制及び駆除、イエダニ・ノミの駆除
6～8畳（10～13平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
12～24畳（20～40平方メートル）あたりに1缶')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 2,'0002',22,1,'7001','0000',1,'アース製薬 アースレッドW ノンスモーク霧タイプ マンション・アパート用 6~8畳用 100МL×3【第2類医薬品】',NULL,'','100ml?3','0000','4901080415622','1,580円（税込）','隙間の奥に隠れたゴキブリを追い出すフラッシング効果と抵抗性ゴキブリに優れた効果を発揮。','＜使用方法＞
1．本品を部屋の中央に置いてください（直接火災報知器に霧があたらない位置）。
ペダルは必ずつま先で踏んでください。
缶底に塗ってある透明樹脂はすべり止めです。はがさないでください。
2．カチッと音がして固定されるまでかかとを浮かし足の指で真上からゆっくりとペダルを踏むか、手で押して作動させてください。薬剤が霧状になって噴射しはじめますので、直ちに部屋から出てください。
3．本品を噴射した後、1～2時間部屋を閉め切った状態にしてください。また、この間、入室することは避けてください。','用法及び用量
○ゴキブリ、屋内塵性ダニ類、イエダニ、ノミ、トコジラミ（ナンキンムシ）の駆除 噴射後1～2時間部屋を閉めきる。
6～8畳（10～13平方メートル）
○ハエ成虫、蚊成虫の駆除 噴射後1～2時間部屋を閉めきる。
12～24畳（20～40平方メートル）')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 3,'0003',22,1,'7001','0000',3,'アース製薬 ダニアースレッド ノンスモーク霧タイプ マンション・アパート用 9~12畳用 100МL×2【第2類医薬品】',NULL,'','100ml?3','0000','4901080415226','1,380円（税込）','アレルギーの原因となるダニやノミの卵から成虫までの全ての成育段階に優れた効果を発揮します。','＜使用方法＞
1．本品を部屋の中央に置いてください。
ペダルは必ずつま先で踏んでください。
缶底に塗ってある透明樹脂はすべり止めです。はがさないでください。
2．カチッと音がして固定されるまでかかとを浮かし足の指で真上からゆっくりとペダルを踏むか、手で押して作動させてください。薬剤が霧状になって噴射しはじめますので、直ちに部屋から出てください。
3．本品を噴射した後、1～2時間部屋を閉め切った状態にしてください。また、この間、入室することは避けてください。','用法及び用量
○屋内塵性ダニ類の増殖抑制及び駆除、イエダニ、ノミの駆除 噴射後1～2時間部屋を閉めきる。
9～12畳（15～20平方メートル）
○ハエ成虫・蚊成虫の駆除 噴射後1～2時間部屋を閉めきる。
18～36畳（30～60平方メートル）')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 4,'0004',22,1,'7001','0000',2,'アース製薬 アースレッド プロα 6~8畳用 10g【第2類医薬品】',NULL,'','10g?3','0000','4901080418715','880円（税込）','アースレッドで最も効きめが強いタイプアースレッドで最も効きめが強いタイプです。','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約22mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。','＜使用量の目安＞
各害虫の駆除には次の使用量をお守りください。
○ゴキブリ・屋内塵性ダニ類・イエダニ・ノミ・トコジラミ（ナンキンムシ）の駆除
6～8畳（10～13平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
12～24畳（20～40平方メートル）あたりに1缶
')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 5,'0005',4,1,'7001','0000',4,'アース製薬 アースレッド プロα 6~8畳用 10g×3【第2類医薬品】',NULL,'','10g','0000','4901080418722','2,345円（税込）','アースレッドで最も効きめが強いタイプアースレッドで最も効きめが強いタイプです。','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約22mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。
','＜使用量の目安＞
各害虫の駆除には次の使用量をお守りください。
○ゴキブリ・屋内塵性ダニ類・イエダニ・ノミ・トコジラミ（ナンキンムシ）の駆除
6～8畳（10～13平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
12～24畳（20～40平方メートル）あたりに1缶')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 6,'0006',4,1,'7001','0000',5,'アース製薬 アースレッドW 6~8畳用 10g×3【第2類医薬品】',NULL,'','10g×3','0000','4901080411624','1,382円（税込）','お部屋まるごと総合害虫駆除水を使うから 少ない煙でよく効く！ お部屋を汚さない！','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約22mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。','＜使用量の目安＞
○ゴキブリ・屋内塵性ダニ類・イエダニ・ノミ・トコジラミ（ナンキンムシ）の駆除
6～8畳（10～13平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
12～24畳（20～40平方メートル）あたりに1缶')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 7,'0007',4,1,'7001','0000',6,'アース製薬 アースレッドW ノンスモーク霧タイプ マンション・アパート用 6~8畳用 100МL【第2類医薬品】',NULL,'','100ml','0000','4901080415615','598円（税込）','隙間の奥に隠れたゴキブリを追い出すフラッシング効果と抵抗性ゴキブリに優れた効果を発揮。','＜使用方法＞
1．本品を部屋の中央に置いてください（直接火災報知器に霧があたらない位置）。
ペダルは必ずつま先で踏んでください。
缶底に塗ってある透明樹脂はすべり止めです。はがさないでください。
2．カチッと音がして固定されるまでかかとを浮かし足の指で真上からゆっくりとペダルを踏むか、手で押して作動させてください。薬剤が霧状になって噴射しはじめますので、直ちに部屋から出てください。
3．本品を噴射した後、1～2時間部屋を閉め切った状態にしてください。また、この間、入室することは避けてください。','用法及び用量
○ゴキブリ、屋内塵性ダニ類、イエダニ、ノミ、トコジラミ（ナンキンムシ）の駆除 噴射後1～2時間部屋を閉めきる。
6～8畳（10～13平方メートル）
○ハエ成虫、蚊成虫の駆除 噴射後1～2時間部屋を閉めきる。
12～24畳（20～40平方メートル）')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 8,'0008',27,1,'7001','0000',7,'アース製薬 アースレッドW 18~24畳用 30gX2【第2類医薬品】',NULL,'','30gX2','0000','4901080411822','2,480円（税込）','お部屋まるごと総合害虫駆除水を使うから 少ない煙でよく効く！ お部屋を汚さない！','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約40mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。','＜使用量の目安＞
○ゴキブリ・屋内塵性ダニ類・イエダニ・ノミ・トコジラミ（ナンキンムシ）の駆除
18～24畳（30～40平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
36～72畳（60～120平方メートル）あたりに1缶')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 9,'0009',27,1,'7001','0000',8,'アース製薬 アースレッドW 30~40畳用 50g【第2類医薬品】',NULL,'','50g','0000','4901080411914','1,830円（税込）','水を使うゴキブリ ダニ ノミお部屋まるごと総合害虫駆除お部屋を汚さない！すみずみまで効く！','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約60mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。','＜使用量の目安＞
○ゴキブリ・屋内塵性ダニ類・イエダニ・ノミ・トコジラミ（ナンキンムシ）の駆除
30～40畳（50～65平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
60～120畳（100～200平方メートル）あたりに1缶
')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 10,'0010',27,1,'7001','0000',NULL,'アース製薬 ダニアースレッド ダニ・ノミ用 12~16畳用 20g×3【第2類医薬品】',NULL,'','20g?3','0000','4901080414526','2,653円（税込）','目に見えないダニの駆除＆予防水を使うから 少ない煙でよく効く！ お部屋を汚さない！ペットのいるご家庭にも！...','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約28mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。','＜使用量の目安＞
各害虫の駆除には次の使用量をお守りください。
○屋内塵性ダニ類の増殖抑制及び駆除、イエダニ・ノミの駆除
12～16畳（20～26平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
24～48畳（40～80平方メートル）あたりに1缶')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 11,'0011',27,1,'7001','0000',9,'アース製薬 アースレッド プロα 12~16畳用 20g×3【第2類医薬品】',NULL,'','20g?3','0000','4901080418821','3,682円（税込）','アースレッドで最も効きめが強いタイプアースレッドで最も効きめが強いタイプです。','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約28mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。','＜使用量の目安＞
各害虫の駆除には次の使用量をお守りください。
○ゴキブリ・屋内塵性ダニ類・イエダニ・ノミ・トコジラミ（ナンキンムシ）の駆除
12～16畳（20～26平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
24～48畳（40～80平方メートル）あたりに1缶
')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 12,'0012',27,1,'7001','0000',10,'アース製薬 ダニアースレッド ノンスモーク霧タイプ マンション・アパート用 9~12畳用 100МL【第2類医薬品】',NULL,'','100ml','0000','4901080415219','802円（税込）','アレルギーの原因となるダニやノミの卵から成虫までの全ての成育段階に優れた効果を発揮します。','＜使用方法＞
1．本品を部屋の中央に置いてください。
ペダルは必ずつま先で踏んでください。
缶底に塗ってある透明樹脂はすべり止めです。はがさないでください。
2．カチッと音がして固定されるまでかかとを浮かし足の指で真上からゆっくりとペダルを踏むか、手で押して作動させてください。薬剤が霧状になって噴射しはじめますので、直ちに部屋から出てください。
3．本品を噴射した後、1～2時間部屋を閉め切った状態にしてください。また、この間、入室することは避けてください。','用法及び用量
○屋内塵性ダニ類の増殖抑制及び駆除、イエダニ、ノミの駆除 噴射後1～2時間部屋を閉めきる。
9～12畳（15～20平方メートル）
○ハエ成虫・蚊成虫の駆除 噴射後1～2時間部屋を閉めきる。
18～36畳（30～60平方メートル）')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 13,'0013',27,1,'7001','0000',NULL,'アース製薬 アースレッドW ノンスモーク霧タイプ マンション・アパート用 9~12畳用 150МL【第2類医薬品】',NULL,'','150МL','0000','4901080415714','802円（税込）','隙間の奥に隠れたゴキブリを追い出すフラッシング効果と抵抗性ゴキブリに優れた効果を発揮。','＜使用方法＞
1．本品を部屋の中央に置いてください（直接火災報知器に霧があたらない位置）。
ペダルは必ずつま先で踏んでください。
缶底に塗ってある透明樹脂はすべり止めです。はがさないでください。
2．カチッと音がして固定されるまでかかとを浮かし足の指で真上からゆっくりとペダルを踏むか、手で押して作動させてください。薬剤が霧状になって噴射しはじめますので、直ちに部屋から出てください。
3．本品を噴射した後、1～2時間部屋を閉め切った状態にしてください。また、この間、入室することは避けてください。','用法及び用量
○ゴキブリ、屋内塵性ダニ類、イエダニ、ノミ、トコジラミ（ナンキンムシ）の駆除 噴射後1～2時間部屋を閉めきる。
9～12畳（15～20平方メートル）
○ハエ成虫、蚊成虫の駆除 噴射後1～2時間部屋を閉めきる。
18～36畳（30～60平方メートル）')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 14,'0014',27,1,'7001','0000',NULL,'アース製薬 ダニアースレッド ノンスモーク霧タイプ マンション・アパート用 6~8畳用 66.7МL×3【第2類医薬品】',NULL,'','66.7МL?3','0000','4901080415127','1,780円（税込）','アレルギーの原因となるダニやノミの卵から成虫までの全ての成育段階に優れた効果を発揮します。','＜使用方法＞
1．本品を部屋の中央に置いてください。
ペダルは必ずつま先で踏んでください。
缶底に塗ってある透明樹脂はすべり止めです。はがさないでください。
2．カチッと音がして固定されるまでかかとを浮かし足の指で真上からゆっくりとペダルを踏むか、手で押して作動させてください。薬剤が霧状になって噴射しはじめますので、直ちに部屋から出てください。
3．本品を噴射した後、1～2時間部屋を閉め切った状態にしてください。また、この間、入室することは避けてください。','用法及び用量
○屋内塵性ダニ類の増殖抑制及び駆除、イエダニ、ノミの駆除 噴射後1～2時間部屋を閉めきる。
6～8畳（10～13平方メートル）
○ハエ成虫・蚊成虫の駆除 噴射後1～2時間部屋を閉めきる。
12～24畳（20～40平方メートル）')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 15,'0015',9,1,'7001','0000',NULL,'アース製薬 アースレッド プロα 12~16畳用 20g【第2類医薬品】',NULL,'','20g','0000','4901080418814','1,380円（税込）','アースレッドで最も効きめが強いタイプアースレッドで最も効きめが強いタイプです。','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約28mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。','＜使用量の目安＞
各害虫の駆除には次の使用量をお守りください。
○ゴキブリ・屋内塵性ダニ類・イエダニ・ノミ・トコジラミ（ナンキンムシ）の駆除
12～16畳（20～26平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
24～48畳（40～80平方メートル）あたりに1缶
')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 16,'0016',9,1,'7001','0000',NULL,'アース製薬 ダニアースレッド ノンスモーク霧タイプ マンション・アパート用 6~8畳用 66.7МL【第2類医薬品】',NULL,'','66.7МL','0000','4901080415110','598円（税込）','アレルギーの原因となるダニやノミの卵から成虫までの全ての成育段階に優れた効果を発揮します。','＜使用方法＞
1．本品を部屋の中央に置いてください。
ペダルは必ずつま先で踏んでください。
缶底に塗ってある透明樹脂はすべり止めです。はがさないでください。
2．カチッと音がして固定されるまでかかとを浮かし足の指で真上からゆっくりとペダルを踏むか、手で押して作動させてください。薬剤が霧状になって噴射しはじめますので、直ちに部屋から出てください。
3．本品を噴射した後、1～2時間部屋を閉め切った状態にしてください。また、この間、入室することは避けてください。','用法及び用量
○屋内塵性ダニ類の増殖抑制及び駆除、イエダニ、ノミの駆除 噴射後1～2時間部屋を閉めきる。
6～8畳（10～13平方メートル）
○ハエ成虫・蚊成虫の駆除 噴射後1～2時間部屋を閉めきる。
12～24畳（20～40平方メートル）')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 17,'0017',9,1,'7001','0000',NULL,'アース製薬 アースレッドW ノンスモーク霧タイプ マンション・アパート用 9~12畳用 150МL×2【第2類医薬品】',NULL,'','150МL?2','0000','4901080415721','1,380円（税込）','隙間の奥に隠れたゴキブリを追い出すフラッシング効果と抵抗性ゴキブリに優れた効果を発揮。','＜使用方法＞
1．本品を部屋の中央に置いてください（直接火災報知器に霧があたらない位置）。
ペダルは必ずつま先で踏んでください。
缶底に塗ってある透明樹脂はすべり止めです。はがさないでください。
2．カチッと音がして固定されるまでかかとを浮かし足の指で真上からゆっくりとペダルを踏むか、手で押して作動させてください。薬剤が霧状になって噴射しはじめますので、直ちに部屋から出てください。
3．本品を噴射した後、1～2時間部屋を閉め切った状態にしてください。また、この間、入室することは避けてください。','用法及び用量
○ゴキブリ、屋内塵性ダニ類、イエダニ、ノミ、トコジラミ（ナンキンムシ）の駆除 噴射後1～2時間部屋を閉めきる。
9～12畳（15～20平方メートル）
○ハエ成虫、蚊成虫の駆除 噴射後1～2時間部屋を閉めきる。
18～36畳（30～60平方メートル）')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 18,'0018',9,1,'7001','0000',NULL,'アース製薬 アースレッドW 12~16畳用 20gx3【第2類医薬品】',NULL,'','20gx3','0000','4901080411723','2,653円（税込）','お部屋まるごと総合害虫駆除水を使うから 少ない煙でよく効く！ お部屋を汚さない！','＜使用方法＞
1．プラスチック容器の中のアルミ袋を取り出してください。水をプラスチック容器の黒破線まで入れてください。水の量：約28mL
2．アルミ袋を開け、缶をそのまま取り出してください。（缶の天面の赤いシールは、はがさないでください。）
3．水を入れたプラスチック容器を部屋の中央に置き、缶の赤いシール面を上にして入れ、リング状の蓋をしてください。水につけてから約1分で蒸散がはじまり、最初の約1分間は勢いよく白煙が上がります。その後薄い白煙が10分程出て蒸散が終了します。
4．缶をセットしたら部屋の外に出て戸を閉めきってください。蒸散開始後、部屋に広がった白煙（蒸散成分）がすみずみまで広がり殺虫効果を発揮するので、約2時間またはそれ以上部屋を閉めきってください。
ハエ・蚊には約30分またはそれ以上で効果がありますが、2時間以上経過してから入室してください。','＜使用量の目安＞
○ゴキブリ・屋内塵性ダニ類・イエダニ・ノミ・トコジラミ（ナンキンムシ）の駆除
12～16畳（20～26平方メートル）あたりに1缶
○ハエ成虫・蚊成虫の駆除
24～48畳（40～80平方メートル）あたりに1缶')
insert ProductTemp (productid,productCode,categoryid,BrandId,PromotionCode,CuponCode,topicId,primaryname,secondaryname,flavor,content,displaycode,barcode,price,description,instruction,extrainformation)  values ( 19,'0019',9,1,'7001','0000',NULL,'アース製薬 ねずみホイホイ 2セット 2セット',NULL,'','2入イ','0000','4901080063700','820円（税込）','強力粘着で、おもしろいほどよくとれるます。深みにはまる谷付きトレー','強力粘着で、おもしろいほどよくとれるます。
深みにはまる谷付きトレーなので、大きいねずみ、すばやいねずみも深みにはまり、ガッチリ捕獲します。
屋根付きで、ハウスタイプなので、捕まえたネズミを見ずに、誤って踏みつけても粘着剤が直接体に触れることが無く、粘着剤にホコリや水が付くのを防ぎます。','
ハウスの片面が直角なので壁にピッタリ設置でき、隅を走るねずみも逃しません。
耐水加工なので、台所や風呂場といった湿気のある場所でも型くずれしません。')

create table ProductImageTemp(
   name varchar(max) null,
   productid int null
)
insert ProductimageTemp (name,productid)  values ( '4901080414427-1',1)
insert ProductimageTemp (name,productid)  values ( '4901080414427-2',1)
insert ProductimageTemp (name,productid)  values ( '4901080415622-1',2)
insert ProductimageTemp (name,productid)  values ( '4901080415622-2',2)
insert ProductimageTemp (name,productid)  values ( '4901080415226-1',3)
insert ProductimageTemp (name,productid)  values ( '4901080415226-2',3)
insert ProductimageTemp (name,productid)  values ( '4901080418715-1',4)
insert ProductimageTemp (name,productid)  values ( '4901080418722-1',5)
insert ProductimageTemp (name,productid)  values ( '4901080418722-2',5)
insert ProductimageTemp (name,productid)  values ( '4901080411624-1',6)
insert ProductimageTemp (name,productid)  values ( '4901080411624-2',6)
insert ProductimageTemp (name,productid)  values ( '4901080415615-1',7)
insert ProductimageTemp (name,productid)  values ( '4901080415615-2',7)
insert ProductimageTemp (name,productid)  values ( '4901080411822-1',8)
insert ProductimageTemp (name,productid)  values ( '4901080411822-2',8)
insert ProductimageTemp (name,productid)  values ( '4901080411914-1',9)
insert ProductimageTemp (name,productid)  values ( '4901080414526-1',10)
insert ProductimageTemp (name,productid)  values ( '4901080414526-2',10)
insert ProductimageTemp (name,productid)  values ( '4901080418821-1',11)
insert ProductimageTemp (name,productid)  values ( '4901080418821-2',11)
insert ProductimageTemp (name,productid)  values ( '4901080415219-1',12)
insert ProductimageTemp (name,productid)  values ( '4901080415714-1',13)
insert ProductimageTemp (name,productid)  values ( '4901080415127-1',14)
insert ProductimageTemp (name,productid)  values ( '4901080415127-2',14)
insert ProductimageTemp (name,productid)  values ( '4901080418814-1',15)
insert ProductimageTemp (name,productid)  values ( '4901080415110-1',16)
insert ProductimageTemp (name,productid)  values ( '4901080415721-1',17)
insert ProductimageTemp (name,productid)  values ( '4901080415721-2',17)
insert ProductimageTemp (name,productid)  values ( '4901080411723-1',18)
insert ProductimageTemp (name,productid)  values ( '4901080411723-2',18)
insert ProductimageTemp (name,productid)  values ( '4901080063700-1',19)
insert ProductimageTemp (name,productid)  values ( '4901080063700-2',19)