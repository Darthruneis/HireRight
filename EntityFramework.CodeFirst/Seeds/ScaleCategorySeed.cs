using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using HireRight.Persistence.Models.CompanyAggregate;
using Newtonsoft.Json;

namespace HireRight.Persistence.Seeds
{
    public static class ScaleCategorySeed
    {
        public static List<ScaleCategory> Seed()
        {
            var categories = new List<ScaleCategory>();
            categories.Add(new ScaleCategory(1, "Achievement Drive", "Achievement Drive measures the degree to which the individual is likely to be competitive and driven to be the best. This characteristic is important for jobs where the attainment of established goals and benchmarks are important (e.g., sales jobs). It is also important for jobs where there may be competition within departments or between coworkers and positions where the individual is expected to grow and advance to higher levels within the organization."));
            categories.Add(new ScaleCategory(2, "Artistic", "The Artistic environment requires working with and through various media to express oneself. Typical artistic occupations include actor, musician, copywriter, architect, photographer. The Artistic scale measures the degree to which the candidate enjoys or has an interest in working in this environment."));
            categories.Add(new ScaleCategory(3, "Assertiveness", "Assertiveness measures the degree to which the individual is likely to assert him/herself, speak his/her mind and enjoy taking control or the lead in group situations. This characteristic is important for jobs where a strong personality is a plus (e.g., most sales jobs and managerial positions)."));
            categories.Add(new ScaleCategory(4, "Attention to Detail", "Attention to Detail measures the degree to which the individual can quickly and accurately compare two strings of letters and/or numbers much like tasks that involve verifying information. This ability is important for most clerical jobs. It is also appropriate for jobs that require proofing tasks."));
            categories.Add(new ScaleCategory(5, "C5 Business: Commitment", "Commitment measures the degree to which the individual is mentally tough, has the internal drive to be the best, exhibits a winning attitude and is willing to do what it takes to succeed."));
            categories.Add(new ScaleCategory(6, "C5 Business: Competitiveness", "Competitiveness measures the degree to which the individual values winning. The degree to which the individual is not easily satisfied and feels that winning or being the best means everything."));
            categories.Add(new ScaleCategory(7, "C5 Business: Conscientiousness", "Conscientiousness measures the degree to which the individual can be counted on to be timely and reliable. The degree to which the individual thinks things through, is organized, responsible and achievement driven."));
            categories.Add(new ScaleCategory(8, "C5 Business: Control", "Control measures the degree to which the individual feels he/she is in control during tough situations, is self confident and is not afraid of high pressure situations."));
            categories.Add(new ScaleCategory(9, "C5 Business: Cooperativeness", "Cooperativeness measures the degree to which the individual is a team player, is willing to sacrifice self for the good of the team, trusts those around him/her and does not engage in conflict with team members or management."));
            categories.Add(new ScaleCategory(10, "Call Center (Sales) Achievement Drive", "Call Center Sales - Achievement Drive measures the degree to which the individual is motivated, has a strong desire to meet his/her sales goals and has a desire to be the best."));
            categories.Add(new ScaleCategory(11, "Call Center (Sales) Assertiveness", "Call Center Sales - Assertiveness measures the degree to which the individual can lead a conversation, is able to maintain control of the interaction. Enjoys taking a leadership role."));
            categories.Add(new ScaleCategory(12, "Call Center (Sales) Positive Attitude", "Call Center Sales - Positive Attitude measures the degree to which the individual has a positive outlook and can bounce back from negative outcomes quickly. Is open to change and is flexible."));
            categories.Add(new ScaleCategory(13, "Call Center (Sales) Reliability", "Call Center Sales - Reliability measures the degree to which the individual can be counted on to get the job done, is organized, punctual and dependable."));
            categories.Add(new ScaleCategory(14, "Call Center (Sales) Self Confidence", "Call Center Sales - Self Confidence measures the degree to which the individual believes in self, is not bothered by rejection and is confident in his/her decision making."));
            categories.Add(new ScaleCategory(15, "Call Center (Sales) Service Ability", "Call Center Sales - Service Ability measures the degree to which the individual is service oriented and enjoys socializing and helping others with a smile."));
            categories.Add(new ScaleCategory(16, "Call Center (Service) Customer Relations", "Call Center CS - Customer Relations measures the degree to which the individual is friendly, people-oriented and exhibits excellent interpersonal skills when interacting with customers. This characteristic is important for all call center jobs that involve interacting with customers."));
            categories.Add(new ScaleCategory(17, "Call Center (Service) Stress Management", "Call Center CS - Stress Management measures the degree to which the individual is likely to demonstrate patience and calmness during times of conflict and pressure at work. This characteristic is appropriate for most call center jobs."));
            categories.Add(new ScaleCategory(18, "Call Center (Service) Team Player", "Call Center CS - Team Player measures the degree to which the individual is likely to cooperate in all aspects of his/her work relationships including working in harmony with others to achieve a common goal. This characteristic is important for call center jobs requiring interaction and cooperation among coworkers."));
            categories.Add(new ScaleCategory(19, "Call Center (Service) Willingness to Help", "Call Center CS - Willingness to Help measures the degree to which the individual is service-oriented and is likely to go out of his/her way to help customers. This characteristic is important for all call center-related jobs that involve interacting with customers."));
            categories.Add(new ScaleCategory(20, "Candidness", "Candidness measures the degree to which the individual is likely to be candid and accurate in his/her responses and is therefore not trying to outsmart the test in an effort to present him/herself in a more favorable light. The results of this scale should be used as a \"red flag\" to make you aware that some candidates may be trying to \"fake\" the test. For example, if a candidate scores High on the behavioral scales in this assessment and he/she scores Low on Candidness, there is the chance that he was able to elevate his/her scores on the assessment by trying to make him/herself look good. High behavioral scale scores that are accompanied by Low Candidness scores warrants some additional follow-up during the interview or reference checks to confirm the candidate's behavioral scale scores are in line with his/her previous work history."));
            categories.Add(new ScaleCategory(21, "Can-Do Attitude", "Can-Do Attitude measures the degree to which the individual consistently approaches work duties and projects with a positive attitude. Is always optimistic. Seldom worries and always exhibits a positive demeanor."));
            categories.Add(new ScaleCategory(22, "Conventional", "The Conventional environment requires working with numbers, comparing and/or proofing information, and the ability to use office equipment. It is generally a structured environment with specific tasks to be accomplished. It involves attention to detail and speed and accuracy when performing perceptual tasks. Examples of positions within this environment are secretaries, file clerks and bookkeepers. The Conventional scale measures the degree to which the candidate enjoys or has an interest in working in this environment."));
            categories.Add(new ScaleCategory(23, "Creativity", "Creativity measures the degree to which the individual is inventive and creative in their thoughts and ideas. This characteristic is important for jobs requiring innovative thinking (e.g., creative positions, marketing, product development and some management jobs)."));
            categories.Add(new ScaleCategory(24, "Customer Care", "Customer Care measures the degree to which the individual is friendly, service oriented and builds customer relationships. He/she is likely to go out of his/her way to assist customers."));
            categories.Add(new ScaleCategory(25, "Drug Free Attitudes", "Drug Free Attitudes measures the degree to which the individual is likely to be free of illegal drug use related problems that will affect his/her work. This characteristic is important for most jobs, but especially those involving the use a machinery or equipment where the safety of the employee and coworkers could be at risk."));
            categories.Add(new ScaleCategory(26, "Energy", "Energy measures the degree to which an individual is likely to demonstrate energy, passion and vitality throughout the workday. This characteristic is important for most fast paced jobs and jobs involving multi-tasking."));
            categories.Add(new ScaleCategory(27, "Enterprising", "The Enterprising Environment requires coordinating the work of others to accomplish specific goals. Although interaction with others is required, it is generally to control, influence or dominate. This environment generally involves the hierarchical work structures that are usually associated with large organizations. Examples of occupations within this environment are sales and management positions. The Enterprising scale measures the degree to which the candidate enjoys or has an interest in working in this environment."));
            categories.Add(new ScaleCategory(28, "Extraversion", "Extraversion measures the degree to which the individual is likely to be outgoing, sociable and assertive in his/her interactions. This characteristic is important for jobs requiring strong, outgoing personalities and strong social skills (e.g., sales, marketing and some management jobs)."));
            categories.Add(new ScaleCategory(29, "Flexibility", "Flexibility measures the degree to which the individual is likely to be able to adapt to change and is more open minded than stubborn. This characteristic is important for fast paced jobs where priorities often shift. It is also important for organizations that are in transition or are expecting changes that will affect work duties and responsibilities."));
            categories.Add(new ScaleCategory(30, "Goal Focus", "The degree to which the individual is able to focus on long-term goals regardless of distractions or obstacles that may be encountered."));
            categories.Add(new ScaleCategory(31, "Go-Getter Attitude", "The degree to which the individual is dedicated, shows initiative, has a positive demeanor and exhibits independence.  This characteristic is important for jobs requiring independent work and a self-starter attitude. Also applicable for the ever growing home based jobs."));
            categories.Add(new ScaleCategory(32, "Good Citizen", "Good Citizen measures the degree to which the individual is likely to follow company policies and adhere to rules and procedures established by management."));
            categories.Add(new ScaleCategory(33, "Healthcare - Compassion", "Healthcare - Compassion measures the degree to which the individual is caring and is likely to go out of his/her way to assist patients. This characteristic is important for all healthcare-related jobs."));
            categories.Add(new ScaleCategory(34, "Healthcare - Patient Relations", "Healthcare - Patient Relations measures the degree to which the individual is friendly, people-oriented and exhibits excellent interpersonal skills when interacting with patients. This characteristic is important for all healthcare-related jobs."));
            categories.Add(new ScaleCategory(35, "Healthcare - Stress Tolerance", "Healthcare - Stress Tolerance measures the degree to which the individual is likely to demonstrate patience and calmness during times of conflict and pressure at work. This characteristic is appropriate for most jobs within healthcare."));
            categories.Add(new ScaleCategory(36, "Healthcare - Team Player", "Healthcare -Team Player measures the degree to which the individual is likely to cooperate in all aspects of his/her work relationships including working in harmony with others to achieve a common goal. This characteristic is important for healthcare jobs requiring interaction and cooperation among coworkers."));
            categories.Add(new ScaleCategory(37, "Helping Disposition", "Helping Disposition measures the degree to which an individual is friendly and is likely to go out of his or her way to assist or help customers and/or co-workers. This characteristic is important for most, if not all, jobs."));
            categories.Add(new ScaleCategory(38, "Inspection", "The Inspection score measures the degree to which an individual can quickly and accurately find errors, defects, and/or dissimilarities when comparing two or more objects. This ability is important for most jobs that require inspecting products for errors and/or jobs requiring attention to small visual details."));
            categories.Add(new ScaleCategory(39, "Interpersonal Skills", "Interpersonal Skills measures the degree to which the individual is friendly, people-oriented and exhibits excellent interpersonal skills. This characteristic is important for all jobs that involve interacting with customers."));
            categories.Add(new ScaleCategory(40, "Investigative", "The Investigative environment includes most scientific professions (e.g., physicians, biologists, laboratory technicians, researchers). These occupations require analytical and problem solving skills. They also require a hunger for knowledge. The emphasis is on ideas rather than people. Proficiency in mathematics and statistics is usually required. Intellectual skills in this occupational group are very important for job success. The Investigative scale measures the degree to which the candidate enjoys or has an interest in working in this environment."));
            categories.Add(new ScaleCategory(41, "Kindness", "Kindness measures the degree to which the individual is caring, empathetic and generous towards others."));
            categories.Add(new ScaleCategory(42, "Language Skills", "Language Skills measures the degree to which the individual can quickly and accurately identify errors in word usage, punctuation, spelling and capitalization. This ability is important for most jobs requiring written communication."));
            categories.Add(new ScaleCategory(43, "Leadership", "Leadership measures the degree to which the individual has the necessary interest, ability and disposition necessary to perform in a leadership capacity. Leadership is important for jobs that require the management of others and/or the coordination of the work of others to accomplish the organization's goals. Supervisors, managers and team or group leaders need leadership characteristics to be successful."));
            categories.Add(new ScaleCategory(44, "Light Industrial Math", "Light Industrial Math measures the degree to which this individual has the ability to learn quickly, problem solve and understand basic mathematical concepts. This ability is appropriate for most, if not all, jobs."));
            categories.Add(new ScaleCategory(45, "Math Skills", "Math Skills measures a basic understanding of mathematical computation and concepts (e.g., making change, calculating percentages, applying discounts, basic addition, subtraction, division and multiplication). The ability to solve these types of problems has also been related to speed of learning and general problem solving skills. This test is appropriate for jobs requiring knowledge of basic math functions (e.g., cashiers, tellers, basic accounting, payroll, etc.)."));
            categories.Add(new ScaleCategory(46, "Mathematical and Logical Reasoning", "Mathematical and Logical Reasoning measures the degree to which the individual is likely to reason logically and to understand and solve basic to more complex mathematical, logical and abstract problems. While research has shown that logical reasoning is important for most jobs, this ability is especially critical for jobs requiring analytical problem solving."));
            categories.Add(new ScaleCategory(47, "Mechanical Aptitude Profile -one test can not be part of custom profile", " An objective measure of an individual’s knowledge of general mechanical concepts. Consists of Electrical, Mechanical Movements, Physical Properties, Spatial Reasoning & Measurement"));
            categories.Add(new ScaleCategory(48, "Non-Violent Attitudes", "Non Violent Attitudes measures the degree to which the individual is likely to respect others and not engage in aggressive workplace behaviors such as intentionally damaging company property or resorting to physical or verbal threats."));
            categories.Add(new ScaleCategory(49, "OCEAN - Agreeableness", "The degree to which the individual is eager to help others, is sympathetic, and believes that others will do the same. Low scores indicate a willingness to fight for one's own interests, skepticism and a critical nature."));
            categories.Add(new ScaleCategory(50, "OCEAN - Conscientiousness", "The degree to which the individual is strong willed, acts with purpose and determination. Is precise, punctual and reliable. Very high scores may be associated with being a workaholic, and overly neat. Low scores are associated with being less organized, less exacting in their work and more relaxed in working towards their goals."));
            categories.Add(new ScaleCategory(51, "OCEAN - Extraversion", "The degree to which the individual is sociable, likes people, and enjoys large gatherings. Tends to be assertive, talkative, active and cheerful. Low scores indicate someone who is reserved, independent, even-paced."));
            categories.Add(new ScaleCategory(52, "OCEAN - Non-Negativity", "The degree to which the individual is emotionally stable, calm, can manage stress and has a positive outlook. Low scores indicate less ability to control impulses, worrying behaviors and a pessimistic outlook."));
            categories.Add(new ScaleCategory(53, "OCEAN - Openness", "The degree to which the individual is curious, willing to try new things, is a creative thinker. Low scores indicate conventional behavior, a conservative outlook and a preference for the familiar."));
            categories.Add(new ScaleCategory(54, "PAP: Dedication", "Dedication measures the degree to which the individual is a hard worker. Takes pride in the quality of his/her work and believes that hard work leads to success."));
            categories.Add(new ScaleCategory(55, "PAP: Initiative", "Initiative measures the degree to which the individual attempts to solve problems instead of avoiding them. Is not afraid to be the first to try something new. Always looks for ways to better self. Actively participates in work projects or meetings instead of sitting back and watching."));
            categories.Add(new ScaleCategory(56, "PAP: Open Mindedness", "Open Mindedness measures the degree to which the individual is willing to learn from others and from his/her own mistakes. Is not stubborn. Listens to the needs and advice of others. Is open to new ideas and concepts."));
            categories.Add(new ScaleCategory(57, "PAP: Optimism", "Optimism measures the degree to which the individual consistently approaches work duties and projects with a positive attitude. Is always optimistic. Seldom worries and always exhibits a positive demeanor."));
            categories.Add(new ScaleCategory(58, "Problem Solving Interest", "Problem Solving Interest measures the degree to which the individual enjoys working on mentally challenging tasks, enjoys problem solving and utilizes creative thinking when engaged in problem solving activities. Some examples of jobs where this ability would be appropriate would be jobs in product development, marketing or research-oriented jobs. Also, jobs that require coming up with enhancements to current processes and procedures."));
            categories.Add(new ScaleCategory(59, "Realistic", "The Realistic environment generally requires manual or physical activities. Jobs within this environment require the use of tools, equipment or machines. They generally involve working with things rather than people. These occupations require mechanical and/or technical skills and may require working outdoors. This occupation group usually encompasses blue-collar jobs. The Realistic scale measures the degree to which the candidate enjoys or has an interest in working in this environment."));
            categories.Add(new ScaleCategory(60, "Reasoning", "Reasoning measures the degree to which this individual has the ability to learn quickly, solve problems, and understand basic mathematical concepts. This ability is appropriate for most jobs."));
            categories.Add(new ScaleCategory(61, "Reliability", "Reliability measures the degree to which the individual is likely to be dependable, hardworking and conscientious about the quality of his/her work."));
            categories.Add(new ScaleCategory(62, "Responsibility", "Responsibility measures the degree to which the individual is likely to be dependable, stable, takes responsibility for his/her actions and as a result, is not likely to have attendance problems. This characteristic is appropriate for all jobs."));
            categories.Add(new ScaleCategory(63, "Rules Compliance", "Rules Compliance measures the degree to which the individual is likely to follow company policies and adhere to rules and procedures established by management. This characteristic is appropriate for most, if not all jobs, with special emphasis on jobs requiring much trust (e.g., bank teller, cashier) and positions of authority (security guards, police officers)."));
            categories.Add(new ScaleCategory(64, "Safety", "Safety measures the degree to which the individual is likely to follow company safety rules and procedures, and is cautious and vigilant about avoiding workplace accidents. These characteristics are appropriate for jobs that involve the use of equipment (e.g., tools, forklifts, machinery), including jobs in warehouse, production, assembly and light industrial settings."));
            categories.Add(new ScaleCategory(65, "Safety Profile", "The Elite Safety Profile is a general indicator of the individual's ability to behave responsibly, be safety conscious and follow rules and procedures. This profile is ideal for jobs where safety is a top priority."));
            categories.Add(new ScaleCategory(66, "Self Confidence", "Self Confidence measures the degree to which the individual is likely to be self assured, is not overly affected by what others think of him/her, and is confident in his/her decisions and actions. This characteristic is important for jobs that require independent thought, a self-starter attitude, sales and management."));
            categories.Add(new ScaleCategory(67, "Self Control", "Self Control measures the degree to which the individual remains calm and in control during stressful, high pressure situations."));
            categories.Add(new ScaleCategory(68, "Service", "Service measures the degree to which the individual is service-oriented and is likely to go out of his/her way to help customers. This characteristic is important for all jobs that require providing excellent customer service."));
            categories.Add(new ScaleCategory(69, "SJT - Management - Communication", "SJT Management - Communication measures the degree to which the individual communicates effectively with staff and coworkers. The degree to which he/she actively listens and expresses ideas and concepts clearly and as necessary."));
            categories.Add(new ScaleCategory(70, "SJT - Management - Decision Making", "SJT Management - Decision Making measures the degree to which the individual makes decisions based on facts and not on impulse. The degree to which he/she is fair and just in his/her decision-making process."));
            categories.Add(new ScaleCategory(71, "SJT - Management - Delegation", "SJT Management - Delegation measures the degree to which the individual delegates work projects as appropriate. The degree to which he/she lets staff perform independently without unnecessary supervision."));
            categories.Add(new ScaleCategory(72, "SJT - Management - Employee Relations", "SJT Management - Employee Relations measures the degree to which the individual promotes positive working relationships with staff and handles employee issues responsibly and effectively while gaining employee trust and respect."));
            categories.Add(new ScaleCategory(73, "SJT - Sales - Customer Focus", "SJT Sales - Customer Focus measures the degree to which the individual puts the customer's needs ahead of his/her own needs. The degree to which he/she is customer service-oriented and enjoys interacting with customers."));
            categories.Add(new ScaleCategory(74, "SJT - Sales - Drive and Persistence", "SJT Sales - Drive and Persistence measures the degree to which the individual is motivated to overcome resistance during his/he sales efforts. The degree to which he/she is driven to perform and strives to be the best."));
            categories.Add(new ScaleCategory(75, "SJT - Sales - Listening Skills", "SJT Sales - Listening Skills measures the degree to which the individual is open to learning new experiences. The degree to which he/she listens to the needs of others and uses listening skills to guide sales strategies."));
            categories.Add(new ScaleCategory(76, "SJT - Sales - Sales Strategies", "SJT Sales - Sales Strategies measures the degree to which the individual engages in appropriate sales-oriented behaviors throughout the sales process. For example, establishes realistic expectations, stays organized, addresses prospect concerns and keeps appointments organized."));
            categories.Add(new ScaleCategory(77, "SJT - Service - Conscientiousness", "SJT Service - Conscientiousness measures the degree to which the candidate thinks things through, is organized and reliable. The degree to which he/she is able to make decisions based on careful thought rather than impulse."));
            categories.Add(new ScaleCategory(78, "SJT - Service - Interpersonal Skills", "SJT Service - Interpersonal Skills measures the degree to which the candidate is friendly, people-oriented and exhibits excellent interpersonal skills when interacting with others."));
            categories.Add(new ScaleCategory(79, "SJT - Service - Listening Skills", "SJT Service - Listening Skills measures the degree to which the candidate focuses on the needs of the customer during interactions. The degree to which he/she listens carefully to the individual's needs and attempts to fulfill those needs."));
            categories.Add(new ScaleCategory(80, "SJT - Service - Service-Orientation", "SJT Service - Service Orientation measures the degree to which the candidate is customer focused and is likely to go out of his/her way to help the customer. The ability to remain service-oriented even during difficult customer situations."));
            categories.Add(new ScaleCategory(81, "SJT - Supervisor - Communication", "The degree to which the candidate values communication with his/her employees. The degree to which he/she takes the initiative to communicate important matters to his/her staff and is sympathetic to their needs."));
            categories.Add(new ScaleCategory(82, "SJT - Supervisor - Conscientiousness", "The degree to which the candidate is organized, hard working and dependable. The degree to which he/she thinks things through rather than making decisions based on impulse."));
            categories.Add(new ScaleCategory(83, "SJT - Supervisor - Motivation", "The degree to which the candidate uses effective motivation strategies with his/her staff. The degree to which the candidate values and utilizes praise and reinforcement as motivators for top performance."));
            categories.Add(new ScaleCategory(84, "SJT - Supervisor - Team Orientation", "The degree to which the candidate is able to instill a team spirit among staff members. The degree to which he/she is able to get along with all staff members, is easy-going and flexible."));
            categories.Add(new ScaleCategory(85, "SJT - Team - Confidence", "SJT Team - Confidence measures the degree to which the candidate, although engaged in team interactions, is likely to be self-assured, is not overly affected by what others think of him or her, and is confident in his or her decisions and actions."));
            categories.Add(new ScaleCategory(86, "SJT - Team - Flexibility", "SJT Team - Flexibility measures the degree to which the candidate is likely to be flexible and open to change. The degree to which he/she is more open-minded than stubborn making it possible to compromise when appropriate."));
            categories.Add(new ScaleCategory(87, "SJT - Team - Team Spirit", "SJT Team - Team Spirit measures the degree to which the candidate is likely to be a team player and cooperate in all aspects of his or her work relationships, including working in harmony with others to achieve a common goal."));
            categories.Add(new ScaleCategory(88, "SJT - Team - Trust", "SJT Team - Trust measures the degree to which the candidate is likely to be trusting of others. The degree to which he/she is likely to build close, trusting relationships with coworkers."));
            categories.Add(new ScaleCategory(89, "Social", "The Social environment requires interaction with people as opposed to things. This environment generally involves helping, teaching or providing service to others. This environment is one of warmth and nurturance. Schools, hospitals and charity organizations are examples of social environments. The Social scale measures the degree to which the candidate enjoys or has an interest in working in this environment."));
            categories.Add(new ScaleCategory(90, "Spatial Reasoning", "Spatial Reasoning measures the degree to which the individual is able to mentally manipulate various objects in order to determine how they might best be assembled or how they might best fit together."));
            categories.Add(new ScaleCategory(91, "Stress Management", "Stress Management measures the degree to which the individual is likely to demonstrate patience and stress tolerance during challenging work-related situations. This characteristic is appropriate for jobs requiring interactions with customers, multitasking and jobs in fast paced organizations to name a few examples."));
            categories.Add(new ScaleCategory(92, "Supervision", "Supervision measures the degree to which the individual has the ability and disposition to motivate others, relate well to employees, create a sense of unity among staff, and maintain high levels of employee satisfaction. Supervision skills are important for jobs that require overseeing and managing others."));
            categories.Add(new ScaleCategory(93, "Tables", "Tables measures the degree to which the individual can quickly and accurately refer to information presented in the form of a table and answer questions related to that information. This characteristic is appropriate for jobs requiring looking up information on computer screens, spreadsheets or charts. Most clerical jobs require some degree of this ability."));
            categories.Add(new ScaleCategory(94, "Team Care", "Team Care measures the degree to which the individual is likely to be flexible and compromising in all aspects of his or her work relationships, including working cooperatively with coworkers and customers."));
            categories.Add(new ScaleCategory(95, "Team Player", "Team Player measures the degree to which the individual is likely to cooperate in all aspects of his/her work relationships including working in harmony with others to achieve a common goal. This characteristic is important for jobs requiring interaction and cooperation among coworkers."));
            categories.Add(new ScaleCategory(96, "Trustworthiness", "Trustworthiness measures the degree to which the individual is likely to be honest and trusting of others. This characteristic is important for most, if not all jobs with special emphasis on cash handling jobs and jobs involving confidential or sensitive information."));
            categories.Add(new ScaleCategory(97, "Verbal Reasoning", "Verbal Reasoning measures the degree to which the individual understands relationships among written or spoken words, which includes word comprehension and associations. This ability is especially important for jobs requiring quick and fluid thinking and jobs requiring that conclusions be drawn from moderate to complex verbal or written communications."));
            categories.Add(new ScaleCategory(98, "Work Ethic", "Work Ethic measures the degree to which the individual is likely to be hardworking, reliable, dedicated and punctual. He/she is conscientious, organized and plans ahead."));

            return categories;
        }

        public static List<ScaleCategory> ParseSeed(string xmlPath, string jsonPath = "")
        {
            var json = GetCategoriesJson(xmlPath, jsonPath);
            var categories = JsonConvert.DeserializeObject<List<ScaleCategory>>(json);
            categories = categories.OrderBy(x => x.Title).ToList();
            bool jsonFileOutOfDate = false;
            foreach (ScaleCategory scaleCategory in categories)
                if (scaleCategory.StaticId != (categories.IndexOf(scaleCategory) + 1))
                {
                    scaleCategory.StaticId = categories.IndexOf(scaleCategory) + 1;
                    jsonFileOutOfDate = true;
                }

            if (jsonFileOutOfDate) UpdateJsonFile(categories, jsonPath);

            return categories;
        }

        private static string GetCategoriesJson(string xmlPath, string jsonPath)
        {
            if (File.Exists(jsonPath)) return File.ReadAllText(jsonPath);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlPath);

            XmlNodeList nodeList = xDoc.GetElementsByTagName("tr");
            var categories = new List<ScaleCategory>();

            AggregateException exceptions = null;
            foreach (XmlNode node in nodeList)
            {
                try
                {
                    if (!node.HasChildNodes) continue;

                    string[] nodes = new string[2];
                    for (int i = 0; i < 2; i++)
                        nodes[i] = node.ChildNodes[i].FirstChild.InnerText;

                    string title = nodes[0];
                    string description = nodes[1];

                    categories.Add(new ScaleCategory(1 + categories.Count, title, description));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    if (exceptions == null)
                        exceptions = new AggregateException(ex);

                    exceptions = new AggregateException(ex, exceptions);
                }
            }

            if (exceptions != null)
                throw exceptions;

            return UpdateJsonFile(categories, jsonPath);
        }

        private static string UpdateJsonFile(List<ScaleCategory> categories, string jsonPath)
        {
            string json = JsonConvert.SerializeObject(categories.Select(x => new { x.StaticId, x.Title, x.Description }));
            File.WriteAllText(jsonPath, json);
            return json;
        }
    }
}