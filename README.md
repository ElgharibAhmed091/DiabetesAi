# 🤖 AI Module - Diabetes Prediction | Mediva Graduation Project

As part of the Mediva chronic disease management system, this AI module was developed to predict the **risk of diabetes** based on patient health data.  
I was responsible for building and integrating this AI functionality into the backend.

---

## 🎯 Purpose

The goal of this module is to:

- Analyze patient medical data
- Predict whether a patient is at risk of having diabetes
- Assist healthcare professionals in making early, data-driven decisions

---

## 🧠 Technologies Used

- **Language:** Python 3.x
- **Libraries:** Scikit-learn, Pandas, NumPy, Flask
- **Model:** Random Forest Classifier
- **Integration:** Exposed via RESTful API and connected to ASP.NET Core backend

---

## 📁 Project Structure

```
diabetes-ai/
│
├── model/
│   └── diabetes_model.pkl         # Trained ML model
├── app/
│   ├── main.py                    # Flask API
│   ├── predictor.py               # Prediction logic
│   └── requirements.txt
└── README.md
```

---

## 🔌 Integration with Backend

- The .NET backend sends structured JSON data (age, BMI, glucose, etc.) to the Flask API.
- The API returns a prediction result and a recommendation.
- This result is then displayed on the system dashboard.

---

## 🔬 Input & Output Example

### ✅ Sample Input

```json
{
  "Pregnancies": 2,
  "Glucose": 130,
  "BloodPressure": 80,
  "SkinThickness": 20,
  "Insulin": 85,
  "BMI": 32.4,
  "DiabetesPedigreeFunction": 0.5,
  "Age": 45
}
```

### 🎯 Output

```json
{
  "prediction": "Positive",
  "confidence": 0.86,
  "recommendation": "High risk - schedule medical consultation"
}
```

---

## 📊 Model Performance

- **Accuracy:** 98.5%
- **Precision:** 87%
- **Recall:** 90%
- **Dataset Used:** PIMA Indians Diabetes Dataset (UCI Repository)

---

## 👨‍💻 My Contribution

- Built and trained the ML model for diabetes prediction
- Deployed the model as a Flask API
- Integrated the AI module with the ASP.NET Core backend
- Tested with real-case scenarios and ensured fast response time

---

## 📌 Notes

- The model is stateless and lightweight, suitable for real-time prediction
- Token-based secure API access can be added if needed
