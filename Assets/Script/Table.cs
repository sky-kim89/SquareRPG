﻿namespace MyProjeckt
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class Table
    {
        readonly static private string[] m_NameTables = new string[]
            {
        "Abel", "아벨 ", "생명력",
"Ace", "에이스", "최고의 것",
"Ada", "아다", "번영하고 즐거운 여자",
"Adam", "아담", "기독교 성서에서 하느님이 처음으로 창조한 남자",
"Adela", "아델", "평화로운",
"Adelio", "아델리오", "고결한 스페인 왕자의 아버지",
"Adolph", "아돌프", "늑대",
"Adonis", "아도니스", "여신 아프로디테가 사랑한 미소년",
"Adora", "아도라", "아주 사랑하는",
"Agatha", "아가타", "나무랄 데 없는 정숙한 여인 여자",
"Aggie", "애기", "훌륭한",
"Aida", "아이다", "베르디 오페라의 주인공인 에티오피아 왕녀",
"Ailish", "앨리쉬", "엘리자베스의 모습을 한",
"Aimee", "에이미", "사랑하는 프랑스 친구",
"Alan", "알란", "잘생긴",
"Albert", "앨버트", "빅토리아 여왕의 부군 남자",
"Albino", "앨비노", "피부색소가 결핍된 사람",
"Alex", "알렉스", "인류의 수호자 남자",
"Alexandra", "알렉산드라", "그리스인의 수호자",
"Alfred", "알프레드", "앨프레드 대왕 남자",
"Ali", "알리", "숭고한, 이슬람 4대 칼리프",
"Alice", "앨리스", "진실 여자",
"Alika", "앨리카", "진실한 하와이인",
"Allie", "앨리", "순결한 탄생",
"Aloha", "알로하", "사랑스러운",
"Alvin", "앨빈", "고귀한 친구",
"Amanda", "아만다", "사랑 받을 만한  여자",
"Ami", "아미", "친구",
"Amos", "아모스", "무거운 짐 남자",
"Amy", "에이미", "가장 사랑하는 여자",
"Anais", "아나이스", "은혜로운",
"Andra", "안드라", "강하고 용감한",
"Andrew", "앤드류", "용기있는 영국인",
"Andy", "앤디", "강하고 남자다운 남자",
"Angel", "엔젤", "천사, 귀여운 아이",
"Angelica", "안젤리카", "켈리포니아산 백포도주",
"Anika", "애니카", "매우 아름다운",
"Anna", "안나", "우아함으로 가득 찬 여자",
"Annie", "애니", "여자이름",
"Anthony", "안토니", "칭찬할 만한 가치",
"Apollo", "아폴로", "옛 그리스 로마의 태양신, 굉장한 미남자",
"Aria", "아리아", "오페라 등에서 악기의 반주가 있는 독창곡",
"Ariel", "아리엘", "중세 전설의 공기의 요정",
"Arista", "아리스타", "최고의 그리스인",
"Arnold", "아놀드", "독수리처럼 강한 남자",
"Arvid", "아비드", "폴란드 독수리",
"Asha", "아샤", "생명",
"Aster", "아스터", "영국 별",
"Astin", "아스틴", "별모양의",
"Aurora", "오로라", "새벽 동틀 녘",

"Ava", "아바", "새 같은",
"Baba", "바바", "화요일에 태어난",
"Bailey", "베일리", "성벽으로 둘러쌓인 안뜰",
"Baldy", "발디", "대머리",
"Bambi", "밤비", "오스트리아 작가",
"Barbara", "바바라", "낯선 여자",
"Barbie", "바비", "금발의 플라스틱 인형, 전형적인 미국인",
"Barley", "발리", "보리, 대맥",
"Barney", "바니", "언쟁 남자",
                "Baron", "바론", "독일의 귀족",
"Basil", "바실", "위엄있는, 장엄한",
"Baxter", "박스터", "존경할 만한",
"Beau", "보", "프랑스어로 아름다운",
"Bebe", "베베", "아기",
                "Beck", "벡", "시내 강물",
"Becky", "베키", "마음을 사로잡는 것 여자",
"Belita", "벨리타", "아름다운",
                "Bella", "벨라", "여자이름",
"Belle", "벨", "불어로 아름다움",
"Benecia", "베네치아", "라틴어로 축복받은",
"Benny", "베니", "가장 사랑하는 어린 아들 남자",
"Berg", "버그", "독일어로 산",
"Bessie", "베시", "신성함 여자",
"Biana", "비안나", "숨기고 있는",
"Bianca", "비앙카", "순백의",
"Bibiane", "비비안", "살아있는",
"Billy", "빌리", "경찰봉 남자",
"Bingo", "빙고", "게임의 일종, 야단 법석",
"Bishop", "비숍", "목자",
            "Bliss", "블리스", "다시 없는 기쁨",
"Blondie", "블론디", "금발의 여자",
"Bonita", "보니타", "예쁜",
"Bono", "보노", "모두가 훌륭한",
"Boris", "보리스", "슬라브어로 전사",
"Boss", "보스", "두목",
"Bright", "브라이트", "빛나는, 생기있는",
"Bruno", "브루노", "갈색 머리",
"Buck", "벅", "사슴",
"Buddy", "버디", "친구",
"Bunny", "버니", "작은 토끼",

"Caesar", "시저", "로마 황제",
"Caley", "칼리", "용감한 전사",
"Calix", "칼릭스", "매우 잘생긴",
"Calla", "칼라", "아름다움",
"Callia", "칼리아", "프랑스어로 아름다운",
"Camilla", "카밀라", "로마신화에 등장하는 여걸 여자",
"Captain", "캡틴", "우두머리",
"Cara", "카라", "값이 비싼 물건",
"Carmel", "카멜", "히브리어로 정원",
"Carmen", "카르멘", "아름다운 목소리를 가진 사람 여자",
"Casey", "캐시", "용감하고 경계심 많은",
"Catherine", "캐서린", "순결한",
"Cecil", "세실", "눈 먼",
"Celestyn", "셀레스틴", "폴란드어로 하늘",
"Celina", "셀리나", "달",
"ChaCha", "샤샤", "남아메리카에서 유래된 볼룸댄스",
"Champ", "챔프", "챔피언",
"Charles", "찰스", "남자다운",
"Charlie", "찰리", "백인",
"Chase", "체이스", "사냥꾼",
"Chavi", "샤비", "여자 아이",
"Chelsea", "첼시", "런던의 옛 區이름",
"Cherie", "쉐리", "사랑받는",
"Chilli", "칠리", "칠레고추로 만든 향신료",
"Chloe", "클로에", "여자이름, 전원시에 나오는 양치는 소녀의 이름",
"Chrissy", "크리시", "기독교 신자",
"Chubby", "처비", "토실토실 살찐, 얼굴이 통통한",
"Cindy", "신디", "신데렐라에서 유래",
"Clara", "클라라", "깨끗한",
"Clark", "클락", "성직자",
"Claudia", "클라우디아", "여자이름",
"Cleo", "클레오", "찬양하다 여자",
"Cleta", "클레타", "침침한",
"Cliff", "클리프", "낭떠러지",
"Coco", "코코", "코코야자, 사람의 머리",
"Cody", "코디", "쿠션, 방석",
"Colin", "콜린", "강하고 씩씩한",
"Connie", "코니", "견고하고 변하지 않는 여자",
"Cookie", "쿠키", "작고 납작한 케이크, 매력적인 여자, 귀여운 소녀",
"Corby", "코비", "까마귀같이 어두운",
"Coy", "코이", "수줍어하는",
"Coyote", "코요테", "북미 대초원에 사는 늑대, 망나니",
"Crimson", "크림슨", "진홍색의",
"Crispin", "크리스핀", "곱슬 머리",
"Crystal", "크리스탈", "깨끗한",
"Cutie", "큐티", "귀여운 여자",
"Cyclone", "사이클론", "큰 회오리 바람",
"Cyma", "시마", "그리스어로 번영하다",

"Daisy", "데이지", "아주 좋은 물건, 프랑스 국화 여자",
"Dali", "달리", "스페인 초현실주의 화가",
"Danika", "다니카", "금성",
                "Darby", "다비", "자유로운 남자",
"Daria", "다리아", "여왕 같은",
"Darin", "다린", "값진 선물",
"Dario", "다리오", "유복한",
"Darwin", "다윈", "사랑하는 친구",
"Dave", "데이브", "가장 사랑하는 사람 남자",
"David", "다비드", "이스라엘 제2대왕인 다윗을 일컬음 남자",
"Dean", "딘", "지도자",
"Della", "델라", "고귀한",
"Delling", "델링", "불꽃을 튀기다.",
"Delphine", "델핀", "프랑스어로 꽃으로부터 여자",
"Dennis", "데니스", "와인 애호가 남자",
"Denver", "덴버", "푸른 계곡",
"Derry", "데리", "고대 아일랜드어로 빨간 머리",
"Deva", "데바", "신성한 정신",
"Dexter", "덱스터", "솜씨가 좋은",
"Diallo", "디알로", "아프리카어 굵은",
"Dick", "딕", "강력한",
"Dino", "디노", "단검",
"Dixie", "딕시", "큰냄비, 미 남부 여러 주의 별명",
"Donna", "돈나", "숙녀",
"Doris", "도리스", "그리스의 도리스 지방 여자",
"Dorothy", "도로시", "신의 선물",
"Douglas", "더글라스", "검은 언덕",
"Duke", "듀크", "공작 남자",
"Dustin", "더스틴", "용맹스런 전사",
"Dyllis", "딜리스", "꾸밈없는",

"Eavan", "에반", "고대 아일랜드어로 정의로운 이",
"Ebony", "에보니", "인도산 흑단, 흑단처럼 새까만",
"Echo", "에코", "메아리",
"Edan", "에단", "고대 아일랜드어로 불",
"Edeline", "에델린", "높은 신분으로 태어난",
"Eden", "에덴", "히브리어로 평원",
"Edward", "에드워드", "행복한 인도자",
"Edwin", "에드윈", "성공한 친구",
"Eilis", "엘리스", "고대 아일랜드어로 친절한",
"Eldora", "엘도라", "스페인어로 황금",
"Elf", "엘프", "꼬마요정",
"Elin", "엘린", "행복한",
"Elisha", "엘리샤", "히브리어로 신의 구원자",
"Elizabeth", "엘리자베스", "신을 위해 봉헌하다는 의미 여자",
"Elle", "엘르", "여성",
"Elroy", "엘로이", "프랑스어로 왕",
"Elsa", "엘사", "고귀한 것 여자",
"Elvis", "엘비스", "현명한 왕자",
"Elysia", "엘리시아", "축복받은 집",
"Emilie", "에밀리", "야망이 있는",
"Eric", "에릭", "강력한 통치자 남자",
"Eris", "에리스", "질투의 여신",
"Eros", "에로스", "사랑",
"Esteban", "에스테반", "승리의 왕관",
"Esther", "에스더", "별",
"Eva", "에바", "여자이름",
"Evan", "에반", "젊은 용사",
"Eve", "이브", "하느님이 창조한 최초의 여자 여자",

"Farrell", "파렐", "고대 아일랜드어로 용맹스러운",
"Favian", "파비앙", "용감한 남자",
"Fedora", "페도라", "그리스어로 신성한 선물",
"Felice", "펠리체", "이탈리아어로 행복한",
"Felix", "펠릭스", "운이 좋은 남자",
"Fella", "펠라", "Fellow의 의미",
"Fidelio", "피델리오", "이탈리아어로 성실한",
"Filia", "필리아", "그리스어로 친분",
"Fleta", "플레타", "쾌속의",
"Florence", "플로렌스", "번영하는",
"Floria", "플로리아", "꽃이 만발한",
"Forrest", "포레스트", "숲",
"Freeman", "프리맨", "자유인",

"Gabriel", "가브리엘", "하나님은 나의 힘",
"Gali", "갈리", "히브리어로 저수지",
"Gem", "젬", "보석",
"Gemma", "젬마", "귀중한 돌",
"George", "조지", "영국 왕의 이름",
"Gilbert", "길버트", "프랑스어로 하나님은 나의 힘",
"Gili", "길리", "기뻐하다.",
"Giovanni", "지오반니", "신의 영광",
"Gloria", "글로리아", "영광",
"Goofy", "구피", "바보 같은, '미키마우스'의 개 이름",
"Grace", "그레이스", "신의 은혜",
"Grania", "그라니아", "곡식의 여신",
"Gregory", "그레고리", "야경꾼",

"Haley", "할리", "영웅",
"Halona", "할로나", "운좋은",
"Happy", "해피", "행복한",
"Harley", "할리", "할리街(일류 의사들의 동네)",
"Harmony", "하모니", "아름다운 조화",
"Harold", "해롤드", "군 통치자",
"Harry", "해리", "약탈하다",
"Heba", "헤바", "아라비아어 선물을 주다",
"Helen", "헬렌", "햇불",
"Helia", "헬리아", "그리스어로 태양",
"Hera", "헤라", "하늘의 여왕",
"Hero", "히어로", "영웅",
"Hestia", "헤스티아", "가정이 여신",
"Hollis", "홀리스", "영웅",
"Honey", "허니", "벌꿀, 멋진 것, 훌륭한 것",
"Hope", "호프", "희망, 소망",
"Hubert", "휴버트", "깨끗한 마음",
"Hue", "휴", "베트남어로 꽃",
"Huey", "휴이", "'도널드 덕'에서의 조카 중 한 명",

"Ian", "이안", "존이라는 이름에서 파생",
"Iliana", "일리아나", "그리스어로 밝은",
"Indira", "인디라", "천둥 번개의 신",
"Ingrid", "잉그리드", "목초지",
"Irina", "이리나", "슬라브어로 평화로운",
"Iris", "아이리스", "그리스어로 무지개",
"Isaac", "이삭", "Abraham과 Sarah의 아들이며 Jacob의 아버지",
"Isabel", "이사벨", "스페인어로 신을 위해 바치다.",
"Isadora", "이사도라", "여자이름",
"Isis", "이시스", "그리스어로 가장 강력한 이집트 여신",

"Jace", "제이스", "매력적인",
"Jack", "잭", "사나이, 버릇 없는 놈",
"Jackson", "잭슨", "미국 제7대 대통령 남자",
"Jaclyn", "재클린", "보호하다",
"Jade", "제이드", "녹색 보석",
"Jane", "제인", "은혜로운",
"Jasmine", "쟈스민", "인도원산의 상록관목",
"Jasper", "제스퍼", "벽옥 남자",
"Jefferson", "제퍼슨", "미국 제 3대 대통령",
"Jeffrey", "제프리", "평화의 선물",
"Jenifer", "제니퍼", "웨일즈어로 하얀 물결",
"Jennie", "제니", "파도",
"Jeremy", "제레미", "기원전 6-7세기의 대예언자",
"Jericho", "제리코", "페르시아어로 달의 도시",
"Jerry", "제리", "라틴어로 성스러운",
"Jess", "제스", "남자이름",
"Jessica", "제시카", "히브리어로 부",
"Jessie", "제시", "부유한 사람 여자",
"Jodie", "조디", "찬양하다",
"Johanna", "조안나", "신의 은혜",
"Jolly", "졸리", "즐거운, 유쾌한",
"Jordan", "조르단", "히브리어로 전해오는",
"Joy", "조이", "기쁨",
"Jud", "쥬드", "기도하다",
"Julia", "쥴리아", "라틴어로 젊은",
"Juliana", "쥴리아나", "부드러운 머리결의",
"Juliet", "쥴리엣", "'로미오와 쥴리엣'의 여자 주인공",
"Justin", "져스틴", "진실",

"Kali", "칼리", "어둠의 여신",
"Kara", "카라", "달콤한 멜로디",
"Karena", "카레나", "순수한 것",
"Karis", "카리스", "그리스어로 은혜로운",
"Kassia", "카시아", "폴란드어로 순수한",
"Kate", "케이트", "켈트어로 처녀스런",
"Kellan", "켈란", "강력한",
"Kelley", "켈리", "여전사",
"Kerri", "케리", "신비스러운",
"Kevin", "케빈", "남자이름",
"Kitty", "키티", "작은 고양이",
"Klaus", "클라우스", "독일어로 승리의 인도자",
"Kori", "코리", "그리스어로 소녀",
"Kuper", "쿠퍼", "히브리어로 구리",
"Kyra", "키라", "숙녀 같은",

"Lakia", "라키아", "아랍어로 재산",
"Lala", "랄라", "튤립",
"Lamis", "라미스", "아랍어로 부드러운",
"Lani", "라니", "하와이어로 하늘",
"Lappy", "래피", "무릎에 앉기를 좋아하는 사람",
"Lara", "라라", "유명한",
"Lavina", "라비나", "라틴어로 로마의 여인",
"Lee", "리", "초원",
"Leena", "리나", "조명",
"Lelia", "렐리아", "그리스어로 옳은 말",
"Leo", "레오", "사자자리 남자",
"Leopold", "레오폴드", "독일어로 사랑스런 사람",
"Lev", "레브", "히브리어로 마음",
"Lidia", "리디아", "폴란드어로 아시아에 있는 성",
"Lily", "릴리", "나리, 백합, 순백한 것",
"Lina", "리나", "이탈리아어로 빛",
"Linda", "린다", "예쁜 사람",
"Lisa", "리사", "히브리어로 신에게 바치다",
"Lloyd", "로이드", "회색의 남자",
"Lonnie", "로니", "잘생긴 사람",
"Lottie", "로티", "여자다운, 여성에게 어울리는",
"Louis", "루이스", "프랑스 루이왕 남자",
"Lowell", "로웰", "프랑스어로 사랑받는",
"Lucia", "루시아", "이탈리아어로 광채",
"Lucifer", "루시퍼", "샛별, 금성",
"Lucy", "루시", "여자이름",
"Lukas", "루카스", "그리스어로 빛",
"Luna", "루나", "달의 여신",

"Mabel", "마벨", "나의 아름다운 사람",
"Madonna", "마돈나", "스페인어로 나의 소녀",
"Maggie", "매기", "여자이름",
"Makaio", "마카이오", "하와이어로 신의 선물",
"Malissa", "맬리사", "달콤한 꿀",
"Malo", "말로", "하와이어로 승리자",
"Mana", "마나", "정신적인 선물",
"Mandelina", "만델리나", "사랑스러운",
"Manon", "마농", "프랑스 소설에 나오는 주인공",
"Marcia", "마르시아", "용기",
"Margaret", "마가레트", "라틴어로 진주",
"Mary", "메어리", "성모마리아 여자",
"Mathilda", "마틸다", "힘",
"Maya", "마야", "그리스어로 어머니",
"Melina", "멜리나", "그리스어로 밝은 노랑",
"Meriel", "메리엘", "켈트어로 빛나는 바다",
"Mickey", "미키", "남자이름",
"Mighty", "마이티", "강력한, 힘센",
"Minnie", "미니", "여자이름",
"Miranda", "미란다", "라틴어로 칭찬해 줄 만한",
"Missy", "미시", "아가씨",
"Misty", "미스티", "안개가 짙은, 눈물어린",
"Molly", "몰리", "여자이름",
"Monet", "모네", "프랑스어로 고독한",
"Monica", "모니카", "그리스어로 조언자",
"Morris", "모리스", "남자이름",
"Muffin", "머핀", "둥근빵 모양의 케이크",
"Mulan", "뮬란", "중국어로 목련꽃",
"Murphy", "머피", "감자",

"Nadia", "나디아", "슬라브어로 희망에 찬",
"Nalo", "날로", "아프리카어로 사랑스러운",
"Nami", "나미", "일본어로 파도",
"Nana", "나나", "할머니, 유모",
"Nani", "나니", "그리스어로 예의 바른",
"Naomi", "나오미", "유쾌한 여자",
"Nara", "나라", "그리스어로 행복한",
"Narcisse", "나르시스", "프랑스어로 수선화",
"Navid", "나비드", "좋은 소식",
"Neal", "닐", "챔피온",
"Neema", "니마", "힌두어로 번영하는",
"Nero", "네로", "강력한",
"Nia", "니아", "챔피언",
"Nicholas", "니콜라스", "성니콜라스 남자",
"Nicky", "닉키", "민중의 승리",
"Nina", "니나", "9번째의 여자",

"Odelia", "오델리아", "신에게 맹세하다",
"Olga", "올가", "슬라브어로 성스런",
"Olive", "올리브", "평화의 상징, 올리브 열매",
"Oliver", "올리버", "남자이름",
"Oscar", "오스카", "신성한 힘",

"Pablo", "파블로", "작은",
"Paloma", "팔로마", "스페인어로 비둘기",
"Pamela", "파멜라", "그리스어로 연인",
"Patrick", "패트릭", "귀족",
"Pavel", "파벨", "슬라브어로 작은",
"Peggy", "페기", "진주 여자",
"Pello", "펠로", "그리스어로 돌",
"Penda", "펜다", "슬라브어로 사랑받는",
"Peppi", "페피", "인내하다",
"Petra", "페트라", "돌",
"Phila", "필라", "사랑",
"Phillip", "필립", "사랑하는 것",
"Pinky", "핑키", "연분홍색의",
"Pluto", "플루토", "명왕성",
"Poco", "포코", "약간, 조금씩",
"Polo", "폴로", "4명이 1조가 되어 말을 타고 하는 공치기",
"Pooky", "푸키", "독일어로 귀여운 사람",
"Poppy", "포피", "꽃으로 부터",
"Primo", "프리모", "이탈리아어로 장남",
"Prince", "프린스", "왕자",
"Princess", "프린세스", "공주, 왕비",
"Puffy", "퍼피", "바람이 확부는, 살찐",

"Rabia", "라비아", "아프리카어로 봄",
"Raina", "레이나", "평화로운",
"Ralph", "랄프", "늑대와 같은 조언자",
"Rambo", "람보", "혼자 사는 기술을 터득하고 폭력적에 보복하는 영화주인공",
"Rania", "라니아", "여왕",
"Ravi", "라비", "태양",
"Redford", "레드포드", "붉은 강 넘어",
"Reggie", "레기", "힘있는 통치자 남자",
"Rei", "레이", "법, 규칙",
"Remy", "레미", "프랑스어로 여러신의 어머니로부터 유래",
"Rex", "렉스", "왕",
"Richard", "리차드", "강력한 남자",
"Ricky", "리키", "부유하고 힘센 사람",
"Ringo", "링고", "반지",
"Rio", "리오", "스페인어로 강",
"Risa", "리사", "웃음소리",
"Robbie", "로비", "빛나는 명성 남자",
"Robert", "로버트", "밝은 명성",
"Robin", "로빈", "길들여진 새 남자",
"Rocky", "록키", "바위가 많은, 바위 같은",
"Roja", "로자", "스페인어로 붉은",
"Rollo", "롤로", "남자이름",
"Romeo", "로미오", "'로미오와 쥴리엣' 의 남자주인공",
"Rosie", "로지", "장미 여자",
"Roxy", "록시", "빛나는 새벽",
"Roy", "로이", "빨간머리털이 있는 남자",
"Ruby", "루비", "귀중한 빨간 보석",
"Rudolph", "루돌프", "유명한 늑대 남자",
"Rudy", "루디", "유명한 늑대 남자",
"Ryan", "리안", "어린 왕",

"Sabrina", "사브리나", "여자이름",
"Sally", "샐리", "출격",
"Salvatore", "살바토르", "이탈리아어로 구원자",
"Sam", "샘", "멋있는 사내",
"Samson", "삼손", "히브리어로 태양과 같이 밝은",
"Sandy", "샌디", "모래",
"Sarah", "사라", "여자이름",
"Sasha", "사샤", "배우자",
"Scarlet", "스칼렛", "타는 듯이 붉은",
"Scoop", "스쿠프", "일확천금",
"Sebastian", "세바스챤", "남자이름",
"Selina", "셀리나", "달",
"Selma", "셀마", "공평한",
"Serena", "세레나", "고요한",
"Severino", "세브리노", "엄중한",
"Shaina", "샤이나", "아름다운",
"Shasa", "샤사", "아프리카어로 귀중한 물",
"Sheri", "쉐리", "친애하는",
"Silky", "실키", "명주의, 부드럽고 매끈매끈한",
"Simba", "심바", "아프리카 사자",
"Simon", "사이먼", "그리스도의 열 두 사도의 한사람",
"Sniper", "스니퍼", "도요새 사냥꾼",
"Solomon", "솔로몬", "기원전 10세기 이스라엘의 현왕",
"Sonia", "소니아", "지혜",
"Sonny", "써니", "젊은 남자, 소년",
"Sophie", "소피", "지혜",
"Sora", "소라", "노래하는 새",
"Sparky", "스파키", "활발한, 발랄한, 생생한",
"Spooky", "스푸키", "잘 놀라는, 겁 많은",
"Spotty", "스포티", "반점이 많은, 얼룩덜룩한",
"Stella", "스텔라", "밝은 별 여자",
"Steven", "스티븐", "왕관 남자S",
"Sting", "스팅", "찌르다",
"Storm", "스톰", "폭풍우",
"Sugar", "슈가", "설탕",
"Sunny", "써니", "빛나는",
"Sweetie", "스위티", "기분좋은 애인",
"Sylvester", "실베스터", "숲",
"Sylvia", "실비아", "여자이름",

"Talia", "탈리아", "이탈리아어로 아침 이슬",
"Talli", "탈리", "영웅",
"Tanesia", "타네시아", "월요일에 태어난",
"Tania", "타니아", "불꽃같은 여왕",
"Ted", "테드", "성스런 선물",
"Teenie", "티니", "작은 사람",
"Terra", "테라", "이탈리아어로 대지",
"Tess", "테스", "여자이름",
"Thomas", "토마스", "영국병사",
"Tomo", "토모", "지적인",
"Trisha", "트리샤", "귀부인",
"Trudy", "트루디", "사랑 받는",

"Uba", "우바", "아프리카어로 귀족",
"Umberto", "움베르토", "이탈리아어로 대지의 색깔",

"Valencia", "발렌시아", "용감한 정신",
"Vanessa", "바네사", "나비",
"Velika", "벨리카", "슬라브어로 거대한",
"Vera", "베라", "이태리어로 진실",
"Verdi", "베르디", "이탈리아의 가극작곡가",
"Veronica", "베로니카", "스페인어로 진리",
"Victoria", "빅토리아", "여자이름, 승리의 여신",
"Vincent", "빈센트", "정복자 남자",
"Violet", "바이올렛", "제비꽃",
"Vito", "비토", "이탈리아어로 생명",
"Vivi", "비비", "이탈리아어로 살아있는",

"Waldo", "왈도", "통치자",
"Walter", "월터", "힘있는 전사 남자",
"Weenie", "위니", "프랑크푸르트 소시지, 장애물",
"Wendy", "웬디", "방랑자 여자",
"William", "윌리엄", "영국의 왕 남자",
"Wily", "윌리", "꾀가 많은, 약삭빠른",
"Winston", "윈스톤", "남자이름",
"Woody", "우디", "수목이 우거진",

"Yaro", "야로", "아프리카어로 아들",
"Yeti", "예티", "티베트의 설인(雪人)",
"Yuki", "유키", "눈",

"Zaza", "자자", "히브리어로 이동",
"Zeki", "제키", "터키어로 영리한",
"Zelia", "젤리아", "그리스어로 열중",
"Zena", "제나", "공손한 사람",
"Zenia", "제니아", "인심좋은 여자",
"Zenon", "제논", "이방인",
"Zeppelin", "제플린", "비행선",
"Zeus", "제우스", "올림푸스 산의 최고의 신",
"Zili", "질리", "나의 그림자",
"Zinna", "지나", "창조적인",
"Zizi", "지지", "헝가리어로 신성",
"Zoe", "조우", "여자이름, 프랑스의 연구용 원자"
            };

        static public string[] NameTables
        {
            get { return m_NameTables; }
        }

        static private Color[] m_BobyColors = new Color[5]
        {
        Color.blue,
        Color.red,
        Color.gray,
        Color.yellow,
        Color.green
        };
        static public Color[] BobyColors
        {
            get { return m_BobyColors; }
        }

        static private Color[] m_HeadColors = new Color[3]
        {
        new Color(150f/255f, 105f/255f, 80f/255f),
        new Color(225f/255f, 195f/255f, 175f/255f),
        new Color(245f/255f, 225f/255f, 215f/255f)
        };
        static public Color[] HeadColors
        {
            get { return m_HeadColors; }
        }

        static private Color[] m_HairColors = new Color[5]
        {
        Color.black,
        Color.red,
        Color.gray,
        Color.yellow,
        Color.white
        };

        static public Color[] HairColors
        {
            get { return m_HairColors; }
        }

        static private Color[] m_EyeLColors = new Color[13]
        {
        new Color(0, 0, 0),
        new Color(30f/255f, 30f/255f, 30f/255f),
        new Color(50f/255f, 50f/255f, 50f/255f),
        new Color(1, 1, 1),
        new Color(170f/255f, 120f/255f, 0),
        new Color(30f/255f, 180f/255f, 180f/255f),
        new Color(120f/255f, 40f/255f, 10f/255f),
        new Color(70f/255f, 70f/255f, 70f/255f),
        new Color(15f/255f, 70f/255f, 15f/255f),
        new Color(200f/255f, 200f/255f, 200f/255f),
        new Color(255f/255f, 0, 0),
        new Color(210f/255f, 210f/255f, 10f/255f),
        new Color(240f/255f, 220f/255f, 30f/255f)
        };

        static private Color[] m_EyeRColors = new Color[13]
         {
        new Color(0, 0, 0),
        new Color(30f/255f, 30f/255f, 30f/255f),
        new Color(50f/255f, 50f/255f, 50f/255f),
        new Color(0, 0, 0),
        new Color(170f/255f, 120f/255f, 0),
        new Color(30f/255f, 180f/255f, 180f/255f),
        new Color(120f/255f, 40f/255f, 10f/255f),
        new Color(70f/255f, 70f/255f, 70f/255f),
        new Color(15f/255f, 70f/255f, 15f/255f),
        new Color(200f/255f, 200f/255f, 200f/255f),
        new Color(0, 0, 255f/255f),
        new Color(210f/255f, 210f/255f, 10f/255f),
        new Color(240f/255f, 220f/255f, 30f/255f)
         };

        static public Color[] EyeRColors
        {
            get { return m_EyeRColors; }
        }

        static public Color[] EyeLColors
        {
            get { return m_EyeLColors; }
        }

        static public void InitColorObjectPool()
        {
            for (int i = 0; i < m_BobyColors.Length; i++)
            {
                ObjectPool.Instance.GetMaterials(m_BobyColors[i]);
            }
            for (int i = 0; i < m_HeadColors.Length; i++)
            {
                ObjectPool.Instance.GetMaterials(m_HeadColors[i]);
            }
            for (int i = 0; i < m_HairColors.Length; i++)
            {
                ObjectPool.Instance.GetMaterials(m_HairColors[i]);
            }
            for (int i = 0; i < m_EyeRColors.Length; i++)
            {
                ObjectPool.Instance.GetMaterials(m_EyeRColors[i]);
            }
            for (int i = 0; i < m_EyeLColors.Length; i++)
            {
                ObjectPool.Instance.GetMaterials(m_EyeLColors[i]);
            }
        }
    }
}
